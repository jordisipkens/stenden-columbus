package stenden.nl.columbus.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.google.gson.Gson;

import stenden.nl.columbus.GSON.Objects.Location;
import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 26/05/15.
 */
public class TravelDetailFragment extends Fragment {

    private Travel mTravel;

    private ListView LocationsList;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_travel_details, container, false);

        Bundle bundle = getArguments();
        mTravel = new Gson().fromJson(bundle.getString("travel"), Travel.class);

        if(mTravel != null){
            // Fill list of locations.
            Location[] loc = mTravel.getLocations();

        }

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
}
