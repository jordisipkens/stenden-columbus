package stenden.nl.columbus.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.util.regex.Pattern;

import stenden.nl.columbus.GSON.Objects.User;
import stenden.nl.columbus.MainActivity;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 19/05/15.
 */
public class AccountFragment extends Fragment implements View.OnClickListener{

    // Make sure the email filled in is correct.
    public final Pattern EMAIL_ADDRESS_PATTERN = Pattern.compile(
            "[a-zA-Z0-9\\+\\.\\_\\%\\-\\+]{1,256}" +
                    "\\@" +
                    "[a-zA-Z0-9][a-zA-Z0-9\\-]{0,64}" +
                    "(" +
                    "\\." +
                    "[a-zA-Z0-9][a-zA-Z0-9\\-]{0,25}" +
                    ")+"
    );

    private Button back, save;
    private EditText username, email, firstname, lastname;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    /**
     * This method will inflate the view and return it.
     */
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_account, container, false);

        // Find buttonview
        back = (Button) v.findViewById(R.id.back);
        save = (Button) v.findViewById(R.id.save);

        // Find edittext
        username = (EditText) v.findViewById(R.id.username);
        email = (EditText) v.findViewById(R.id.email);
        firstname = (EditText) v.findViewById(R.id.firstname);
        lastname = (EditText) v.findViewById(R.id.lastname);

        // Set text of EditTexts from MainActivity.user
        fillFields();

        // Set listeners
        back.setOnClickListener(this);
        save.setOnClickListener(this);

        return v;
    }

    @Override
    public void onStart() {
        super.onStart();
    }

    @Override
    public void onPause() {
        super.onPause();
    }

    @Override
    public void onStop() {
        super.onStop();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
    }

    @Override
    public void onDetach() {
        super.onDetach();
    }

    /**
     * Make sure all the onclicklisteners will come here.
     * Make a switch case on the view ID to check which view has been clicked.
     */
    @Override
    public void onClick(View v) {
        switch(v.getId()){
            case R.id.back:
                getActivity().getSupportFragmentManager().beginTransaction()
                        .replace(R.id.container, new HomeFragment())
                        .addToBackStack("home").commit();
                break;
            case R.id.save:
                if(checkEmail(email.getText().toString())) {
                    // Save information form EditText to the account variable in MainActivity.

                    User user = new User();

                    // Should NOT change.
                    user.setPassword(MainActivity.user.getPassword());
                    user.setId(MainActivity.user.getId());

                    // Can change so just what is in the editText
                    // To-do -> Email check and check if fields are not empty.
                    user.setUser(username.getText().toString());
                    user.setEmail(email.getText().toString());
                    user.setName(firstname.getText().toString());
                    user.setLastName(lastname.getText().toString());

                    MainActivity.user = user;

                    new Toast(getActivity()).makeText(getActivity(), "Gegevens zijn opgeslagen", Toast.LENGTH_SHORT).show();

                } else {
                    new Toast(getActivity()).makeText(getActivity(), "Geen geldige email.", Toast.LENGTH_LONG).show();
                }

                break;
        }
    }

    /**
     * Method to fill the fields according to the current user.
     * Always check if field is null or not to prevent crashes.
     */
    private void fillFields(){
        if(MainActivity.user != null) {
            if (MainActivity.user.getUser() != null) {
                username.setText(MainActivity.user.getUser());
            }
            if (MainActivity.user.getEmail() != null) {
                email.setText(MainActivity.user.getEmail());
            }
            if (MainActivity.user.getName() != null) {
                firstname.setText(MainActivity.user.getName());
            }
            if (MainActivity.user.getLastName() != null) {
                lastname.setText(MainActivity.user.getLastName());
            }
        }
    }

    /**
     * Method to check if email is valid.
     */
    private boolean checkEmail(String email) {
        return EMAIL_ADDRESS_PATTERN.matcher(email).matches();
    }
}
