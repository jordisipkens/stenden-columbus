package stenden.nl.columbus.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import stenden.nl.columbus.GSON.Objects.User;
import stenden.nl.columbus.MainActivity;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 19/05/15.
 */
public class AccountFragment extends Fragment implements View.OnClickListener{

    private Button back, save;
    private EditText username, email, firstname, lastname;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

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
        // To-do ^^

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

    @Override
    public void onClick(View v) {
        switch(v.getId()){
            case R.id.back:
                getActivity().getSupportFragmentManager().popBackStack();
                break;
            case R.id.save:
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


                break;
        }
    }
}
