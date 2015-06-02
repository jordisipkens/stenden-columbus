package stenden.nl.columbus;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Base64;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.Response;

import java.util.HashMap;
import java.util.Map;

import stenden.nl.columbus.Encryptie.CryptLib;
import stenden.nl.columbus.GSON.GsonRequest;
import stenden.nl.columbus.GSON.Objects.Token;
import stenden.nl.columbus.GSON.VolleyHelper;

/**
 * Created by Jordi on 27/05/15.
 */
public class LoginScreen extends Activity implements View.OnClickListener {
    private Button login;
    private EditText mUser, mPass;
    private String base_url = "http://columbus-webservice.azurewebsites.net/";
    private String login_call = "api/User/Login";

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

        //Intent intent = new Intent(getApplicationContext(), MainActivity.class);
        //startActivity(intent);

    }

    private void apiRequest(String header){
        Map<String, String> headers = new HashMap<String, String>();
        headers.put("Authorization", "Basic QzBsdW1idXM6Y3hUdDdxSUNxcVpXUXpHMXVUVGdidz09");
        String FULL_URL = base_url + login_call;
        VolleyHelper.getInstance(getApplicationContext()).addToRequestQueue(
                new GsonRequest<>(FULL_URL, Token.class, headers, new Response.Listener<Token>() {
                    public void onResponse(Token s) {
                        Token token = s;
                        if(token != null){
                            Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                            startActivity(intent);
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
}