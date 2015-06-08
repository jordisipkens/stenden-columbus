package stenden.nl.columbus.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

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

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

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

            cancel.setOnClickListener(this);
            submit.setOnClickListener(this);
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

    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.cancel:
                break;
            case R.id.submit:
                break;
        }
    }
}
