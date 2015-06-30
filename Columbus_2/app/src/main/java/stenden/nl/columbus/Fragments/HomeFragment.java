package stenden.nl.columbus.Fragments;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.android.volley.Response;
import com.google.gson.Gson;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import stenden.nl.columbus.GSON.GsonRequest;
import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.GSON.Objects.Travelogue;
import stenden.nl.columbus.GSON.VolleyHelper;
import stenden.nl.columbus.MainActivity;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 19/05/15.
 *
 * Fragment for the home screen of the user.
 * Should display travels when they are bounded to the current account.
 * Also uses a GET method to get the Travelogues already created on the current account.
 */
public class HomeFragment extends Fragment {

    private ListView mTravelList;
    private TravelAdapter mAdapter;
    private Travel[] mTravels;

    public HomeFragment() {
        super();
    }
    /**
     * Make sure all the important stuff happens here before setting the view.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // GET all travelogues from user.
        if (MainActivity.loginResponse.getUser() != null && MainActivity.travelogues == null) {
            Map<String, String> headers = new HashMap<String, String>();
            headers.put("Token", MainActivity.loginResponse.getToken());


            String full_url = MainActivity.BASE_URL + MainActivity.ALL_TRAVELOGUE_URL
                    + "?userID=" + MainActivity.loginResponse.getUser().getId();

            // Get all travelogues from the user.
            VolleyHelper.getInstance(getActivity()).addToRequestQueue(
                    new GsonRequest<>(full_url,
                            Travelogue[].class, headers, new Response.Listener<Travelogue[]>() {
                        public void onResponse(Travelogue[] tempTravel) {
                            // Revert the array to a arraylist for global usage benefits.
                            if (tempTravel != null) {
                                MainActivity.travelogues = new ArrayList<>();
                                for (Travelogue x : tempTravel) {
                                    MainActivity.travelogues.add(x);
                                }
                            }
                        }
                    }, getActivity()));
        }
    }
    /**
     * This method will inflate the view and return it.
     *
     * Get the travelogues in the View so you can set the adapter accordingly.
     */
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_main, container, false);

        mTravelList = (ListView) v.findViewById(R.id.travel_list);

        // If travels is already filled, avoid the api call.
        // Else make sure to GET all the travels from the user.
        if (MainActivity.loginResponse.getUser() != null && MainActivity.travels == null) {
            Map<String, String> headers = new HashMap<String, String>();
            headers.put("Token", MainActivity.loginResponse.getToken());


            String full_url = MainActivity.BASE_URL + MainActivity.ALL_TRAVELS_URL
                    + "?userID=" + MainActivity.loginResponse.getUser().getId();

            VolleyHelper.getInstance(getActivity()).addToRequestQueue(
                    new GsonRequest<>(full_url,
                            Travel[].class, headers, new Response.Listener<Travel[]>() {
                        public void onResponse(Travel[] tempTravel) {
                            if (tempTravel != null) {
                                // Show the desired lists.
                                MainActivity.travels = tempTravel;

                                mAdapter = new TravelAdapter(MainActivity.travels, getActivity());
                                mTravelList.setAdapter(mAdapter);

                                setAdapterListener();
                            }
                        }
                    }, getActivity()));
        } else if (MainActivity.travels != null) {
            mAdapter = new TravelAdapter(MainActivity.travels, getActivity());
            mTravelList.setAdapter(mAdapter);

            setAdapterListener();
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
    public void onResume() {
        super.onResume();
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
     * Method to open the detail of the selected Travel item.
     */
    private void setAdapterListener() {
        mTravelList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                // new fragment with travel details.
                // Travel must be sent trough to show travel.
                // Serialize the travel and deserialise in the TravelDetailFragment.java
                FragmentManager manager = getActivity().getSupportFragmentManager();
                FragmentTransaction trans = manager.beginTransaction();

                Fragment frag = new TravelDetailFragment();

                Bundle bundle = new Bundle();
                bundle.putString("travel", new Gson().toJson(MainActivity.travels[i]));
                bundle.putInt("TravelID", MainActivity.travels[i].getId());
                frag.setArguments(bundle);

                trans.replace(R.id.container, frag);
                trans.addToBackStack("Detail");
                trans.commit();
            }
        });
    }

    /**
     * Custom BaseAdapter which sets the view of the TravelList.
     * GetView inflates the desired View and sets the items.
     */
    private class TravelAdapter extends BaseAdapter {
        // According to the size of this list, so many items will the ListView have on screen.
        private Travel[] list;
        private Context ctx;
        private View v;
        private TextView startDate, endDate, title;

        public TravelAdapter(Travel[] list, Context ctx) {
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

        @Override
        public View getView(int position, View view, ViewGroup parent) {
            v = LayoutInflater.from(ctx).inflate(R.layout.travel_list_item,
                    parent, false);
            startDate = (TextView) v.findViewById(R.id.travel_start_date);
            endDate = (TextView) v.findViewById(R.id.travel_end_date);
            title = (TextView) v.findViewById(R.id.travels_title);

            title.setText(list[position].getTitle());

            //Format dateTime
            SimpleDateFormat form = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
            SimpleDateFormat sForm = new SimpleDateFormat("dd-MM-yyyy");
            try {
                Date sDate = form.parse(list[position].getStartDate());
                Date eDate = form.parse(list[position].getEndDate());

                startDate.setText(startDate.getText() + sForm.format(sDate));
                endDate.setText(endDate.getText() + sForm.format(eDate));
            } catch (ParseException e) {
                e.printStackTrace();
            }

            return v;
        }
    }
}
