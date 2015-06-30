package stenden.nl.columbus.GSON;

import android.app.Activity;
import android.content.Intent;
import android.util.Log;

import com.android.volley.AuthFailureError;
import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HttpHeaderParser;
import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import java.io.UnsupportedEncodingException;
import java.util.Map;

import stenden.nl.columbus.LoginScreen;
import stenden.nl.columbus.MainActivity;

/**
 * Created by Jordi on 18/05/15.
 */
public class GsonRequest<T> extends Request<T> {
    private final Gson gson = new Gson();
    private Class<T> clazz;
    private Map<String, String> params;
    private final Map<String, String> headers;
    private final Response.Listener<T> listener;

    /**
     * Make a GET request and return a parsed object from JSON.
     *
     * @param url     URL of the request to make
     * @param clazz   Relevant class object, for Gson's reflection
     * @param headers Map of request headers
     */
    public GsonRequest(String url, Class<T> clazz, Map<String, String> headers,
                        Response.Listener<T> listener, final Activity activity) {
        super(Request.Method.GET, url, new Response.ErrorListener() {
            public void onErrorResponse(VolleyError volleyError) {
                if (volleyError != null) {
                    activity.startActivity(new Intent(activity, LoginScreen.class));
                    MainActivity.user = null;
                    MainActivity.travels = null;
                    MainActivity.loginResponse = null;
                    Log.e("GsonRequest", "" + volleyError.getMessage());
                }
            }
        });
        this.clazz = clazz;
        this.headers = headers;
        this.listener = listener;
    }

    /**
     * Is required or else it won't return headers.
     */
    @Override
    public Map<String, String> getHeaders() throws AuthFailureError {
        return headers;
    }

    @Override
    protected void deliverResponse(T response) {
        listener.onResponse(response);
    }

    @Override
    protected Response<T> parseNetworkResponse(NetworkResponse response) {
        try {
            String json = new String(response.data, HttpHeaderParser.parseCharset(response.headers));
            //Log.e("parseNetworkResponse", json);
            return Response.success(gson.fromJson(json, clazz), HttpHeaderParser.parseCacheHeaders(response));
        } catch (UnsupportedEncodingException e) {
            return Response.error(new ParseError(e));
        } catch (JsonSyntaxException e) {
            return Response.error(new ParseError(e));
        }
    }
}
