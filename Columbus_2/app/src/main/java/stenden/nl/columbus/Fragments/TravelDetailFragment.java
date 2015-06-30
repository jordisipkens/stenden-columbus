package stenden.nl.columbus.Fragments;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.TextView;

import com.google.gson.Gson;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import stenden.nl.columbus.GSON.Objects.Location;
import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 26/05/15.
 *
 * Class to show all the locations and details of the selected Travel.
 */
public class TravelDetailFragment extends Fragment {

    private Travel mTravel;

    private ListView locationList;
    private TextView travelTitle, travelDate;
    private Button travelogue;

    private LocationAdapter adapter;

    private int travelID;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    /**
     * Inflate the view and set all the important variables and checks so the correct views are set and shown.
     */
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_travel_details, container, false);

        locationList = (ListView) v.findViewById(R.id.travel_locations);
        travelTitle = (TextView) v.findViewById(R.id.travel_title);
        travelDate = (TextView) v.findViewById(R.id.travel_date);
        travelogue = (Button) v.findViewById(R.id.travelogue);

        Bundle bundle = getArguments();
        mTravel = new Gson().fromJson(bundle.getString("travel"), Travel.class);
        travelID = bundle.getInt("TravelID");

        if (mTravel != null) {
            // Set title and date
            travelTitle.setText(mTravel.getTitle());

            //Format dateTime
            SimpleDateFormat form = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
            SimpleDateFormat sForm = new SimpleDateFormat("dd-MM-yyyy");
            try {
                Date sDate = form.parse(mTravel.getStartDate());
                Date eDate = form.parse(mTravel.getEndDate());

                travelDate.setText(sForm.format(sDate) + " - " + sForm.format(eDate));
            } catch (ParseException e){
                e.printStackTrace();
            }



            // Fill list of locations.
            Location[] loc = mTravel.getLocations();

            adapter = new LocationAdapter(loc, getActivity());

            locationList.setAdapter(adapter);

            // Set onClickListener for button travelogue (Reisverslag)
            travelogue.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Fragment frag = new TravelogueFragment();

                    Bundle bundle = new Bundle();
                    bundle.putInt("travelID", travelID);
                    frag.setArguments(bundle);

                    getActivity().getSupportFragmentManager().beginTransaction()
                            .replace(R.id.container, frag)
                            .addToBackStack("Travelogue").commit();
                }
            });
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

    /**
     * Custom BaseAdapter to show the information trough a custom View.
     */
    private class LocationAdapter extends BaseAdapter {
        // The size of this list will mean the size of the ListView.
        private Location[] list;
        private Context ctx;
        private View v;
        private TextView date, title, address, phoneNumber;
        private Button note;

        public LocationAdapter(Location[] list, Context ctx) {
            this.list = list;
            this.ctx = ctx;
        }

        @Override
        public int getCount() {
            return list.length;
        }

        @Override
        public Object getItem(int i) {
            return list[i];
        }

        @Override
        public long getItemId(int i) {
            return i;
        }

        /**
         * Inflate the View for the list items and set the childviews.
         */
        @Override
        public View getView(int position, View view, ViewGroup parent) {
            v = LayoutInflater.from(ctx).inflate(R.layout.location_list_item,
                    parent, false);
            date = (TextView) v.findViewById(R.id.location_date);
            title = (TextView) v.findViewById(R.id.location_name);
            address = (TextView) v.findViewById(R.id.location_address);
            phoneNumber = (TextView) v.findViewById(R.id.location_phonenumber);
            note = (Button) v.findViewById(R.id.location_note);

            //Format dateTime
            SimpleDateFormat form = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
            SimpleDateFormat sForm = new SimpleDateFormat("dd-MM-yyyy");
            try {
                Date sDate = form.parse(list[position].getDate());

                date.setText(date.getText() + sForm.format(sDate));
            } catch (ParseException e){
                e.printStackTrace();
            }


            title.setText(list[position].getLocationDetails().getName());
            address.setText(address.getText() + list[position].getLocationDetails().getAddress());
            phoneNumber.setText(phoneNumber.getText() + list[position].getLocationDetails().getPhone());

            final Location loc = list[position];

            note.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Fragment frag = new LocationNoteFragment();

                    Bundle bundle = new Bundle();
                    bundle.putInt("TravelID", travelID);
                    bundle.putInt("LocID", loc.getId());

                    frag.setArguments(bundle);

                    getActivity().getSupportFragmentManager().beginTransaction()
                            .replace(R.id.container, frag)
                            .addToBackStack("LocationNote").commit();
                }
            });

            // Make frag ready for when button is clicked.
            final Fragment fragment = new MapFragment();

            Bundle bundle = new Bundle();
            bundle.putDoubleArray("coordinates", new double[]{loc.getLocationDetails().getLatlng().getLat(), loc.getLocationDetails().getLatlng().getLng()});
            bundle.putString("locTitle", loc.getLocationDetails().getName());
            fragment.setArguments(bundle);

            ImageButton button = (ImageButton) v.findViewById(R.id.map_button);
            button.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    getActivity().getSupportFragmentManager().beginTransaction()
                            .replace(R.id.container, fragment)
                            .addToBackStack("Maps").commit();
                }
            });

            return v;
        }
    }
}
