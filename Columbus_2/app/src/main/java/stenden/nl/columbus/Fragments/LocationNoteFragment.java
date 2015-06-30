package stenden.nl.columbus.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import stenden.nl.columbus.GSON.Objects.Location;
import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.MainActivity;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 08/06/15.
 */
public class LocationNoteFragment extends Fragment implements View.OnClickListener{

    private Location loc;

    private Button cancel, submit;
    private EditText notes;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }
    /**
     * This method will inflate the view and return it.
     *
     * And if necessary set the correct views and lists.
     */
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_note, container, false);

        int travelID = getArguments().getInt("TravelID");
        int locID = getArguments().getInt("LocID");

        for(Travel x : MainActivity.travels){
            if(travelID == x.getId()){
                for(Location l : x.getLocations()){
                    if(locID == l.getId()){
                        loc = l;
                    }
                }
            }
        }

        if(loc != null){
            cancel = (Button) v.findViewById(R.id.cancel);
            submit = (Button) v.findViewById(R.id.submit);
            notes = (EditText) v.findViewById(R.id.editText);


            cancel.setOnClickListener(this);
            submit.setOnClickListener(this);
            notes.setText(loc.getNote());
        }

        return v;
    }

    @Override
    public void onResume() {
        super.onResume();
    }

    @Override
    public void onStart() {
        super.onStart();
    }

    @Override
    public void onStop() {
        super.onStop();
    }

    @Override
    public void onPause() {
        super.onPause();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
    }

    /**
     * Make sure all the onclicklisteners will come here.
     * Make a switch case on the view ID to check which view has been clicked.
     */
    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.cancel:
                getActivity().getSupportFragmentManager().popBackStack();
                break;
            case R.id.submit:
                int travelID = getArguments().getInt("TravelID");
                int locID = getArguments().getInt("LocID");

                if(loc != null){
                    for(int i = 0; i < MainActivity.travels.length; i++){
                        if(travelID == MainActivity.travels[i].getId()){
                            for(int x = 0; x < MainActivity.travels[i].getLocations().length; x++){
                                if(locID == MainActivity.travels[i].getLocations()[x].getId()){
                                    MainActivity.travels[i].getLocations()[x].setNote(notes.getText().toString());
                                    Toast toast = new Toast(getActivity()).makeText(getActivity(), "Notitie is succesvol opgeslagen", Toast.LENGTH_SHORT);
                                    toast.show();
                                }
                            }
                        }
                    }
                }
                break;
        }
    }
}
