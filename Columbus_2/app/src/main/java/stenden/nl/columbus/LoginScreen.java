package stenden.nl.columbus;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Base64;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.Response;
import com.google.gson.Gson;

import java.util.HashMap;
import java.util.Map;

import stenden.nl.columbus.Encryptie.CryptLib;
import stenden.nl.columbus.GSON.GsonRequest;
import stenden.nl.columbus.GSON.Objects.LoginResponse;
import stenden.nl.columbus.GSON.VolleyHelper;

/**
 * Created by Jordi on 27/05/15.
 */
public class LoginScreen extends Activity implements View.OnClickListener {
    private Button login;
    private EditText mUser, mPass;

    //AES Encryption fields
    //private static final String key = "C0lumbu5";
    private static String salt;
    private static int pswdIterations = 65536;
    private static int keySize = 256;
    private byte[] ivBytes;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.login_screen);

        SharedPreferences settings = getSharedPreferences(MainActivity.PREFS_NAME, 0);
        LoginResponse loginResponse = new LoginResponse();
        loginResponse.setToken(settings.getString("loginResponse", null));
        if(loginResponse.getToken() != null){
            MainActivity.loginResponse = loginResponse;
            startActivity(new Intent(this, MainActivity.class));
            finish();
        }

        login = (Button) findViewById(R.id.login);
        mUser = (EditText) findViewById(R.id.username);
        mPass = (EditText) findViewById(R.id.password);
        login.setOnClickListener(this);

    }

    @Override
    protected void onStart() {
        super.onStart();
    }

    @Override
    protected void onRestart() {
        super.onRestart();
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    @Override
    protected void onPause() {
        super.onPause();
    }

    @Override
    protected void onStop() {
        if(MainActivity.loginResponse != null){
            SharedPreferences settings = getSharedPreferences(MainActivity.PREFS_NAME, 0);
            SharedPreferences.Editor editor = settings.edit();

            editor.putString("loginResponse", MainActivity.loginResponse.getToken()).commit();
        }
        super.onStop();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.login:

                String username = mUser.getText().toString();
                String password = mPass.getText().toString();

                if(!username.equalsIgnoreCase("") && !password.equalsIgnoreCase("") && username != null && password != null) {
                    Login(username, password);
                } else {
                    Toast toast = Toast.makeText(getApplicationContext(), "Please fill in all fields.", Toast.LENGTH_SHORT);
                    toast.show();
                }
        }
    }

    private void Login(String user, String pass) {
        pass = Encrypt(pass);
        pass = pass.substring(0, pass.length() -1);

        String fullHeader = user + ":" + pass;
        fullHeader = Base64(fullHeader);
        fullHeader = fullHeader.substring(0, fullHeader.length() - 1);

        if(fullHeader != null){
            apiRequest(fullHeader);
        }

    }

    private void apiRequest(String header){
        Map<String, String> headers = new HashMap<String, String>();
        headers.put("Authorization", "Basic " + header);
        String FULL_URL = MainActivity.BASE_URL + MainActivity.LOGIN_URL;
        VolleyHelper.getInstance(getApplicationContext()).addToRequestQueue(
                new GsonRequest<>(FULL_URL, LoginResponse.class, headers, new Response.Listener<LoginResponse>() {
                    public void onResponse(LoginResponse loginResponse) {
                        if(loginResponse.getToken() != null){
                            // Sharedpreferences
                            SharedPreferences settings = getSharedPreferences(MainActivity.PREFS_NAME, 0);
                            SharedPreferences.Editor editor = settings.edit();

                            editor.putString("loginResponse", loginResponse.getToken()).commit();
                            editor.putString("user", new Gson().toJson(loginResponse.getUser())).commit();

                            // Start MainActivity
                            MainActivity.user = loginResponse.getUser();
                            MainActivity.loginResponse = loginResponse;
                            startActivity(new Intent(getApplicationContext(), MainActivity.class));
                            finish();
                        }
                    }
                }));
    }

    private String Base64(String plainText){
        try {
            //CryptLib crypt = new CryptLib();
            String output = plainText;
            output = Base64.encodeToString(output.getBytes(), Base64.DEFAULT);
            return output;
        } catch (Exception e){
            return null;
        }
    }

    private String Encrypt(String plainText) {
        try {
            CryptLib crypt = new CryptLib();
            String output = "";
            String key = CryptLib.SHA256("C0lumbu5", 32); //32 bytes = 256 bit
            output = crypt.encrypt(plainText, key, ""); //encrypt

            return output;
        } catch (Exception e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return null;
        }
    }

    @Override
    public void onBackPressed() {
        System.exit(0);
    }
}