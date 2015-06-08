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

import java.util.HashMap;
import java.util.Map;

import stenden.nl.columbus.GSON.GsonRequest;
import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.GSON.VolleyHelper;
import stenden.nl.columbus.MainActivity;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 19/05/15.
 * <p/>
 * Fragment for the home screen of the user.
 * Should display travels when they are bounded to the current account.
 */
public class HomeFragment extends Fragment {

    private ListView mTravelList;
    private TravelAdapter mAdapter;
    private Travel[] mTravels;

    public HomeFragment() {
        super();
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_main, container, false);

        mTravelList = (ListView) v.findViewById(R.id.travel_list);

        // Call all travels from user.
        if(MainActivity.loginResponse.getUser() != null) {
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
                                mTravels = tempTravel;
                                MainActivity.travels = mTravels;

                                mAdapter = new TravelAdapter(mTravels, getActivity());
                                mTravelList.setAdapter(mAdapter);

                                setAdapterListener();
                            }
                        }
                    }));
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
                bundle.putString("travel", new Gson().toJson(mTravels[i]));
                bundle.putInt("TravelID", mTravels[i].getId());
                frag.setArguments(bundle);

                trans.replace(R.id.container, frag);
                trans.addToBackStack("Detail");
                trans.commit();
            }
        });
    }

    private class TravelAdapter extends BaseAdapter {
        private Travel[] list;
        private Context ctx;
        private View v;
        private TextView startDate, endDate;

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

            startDate.setText(list[position].getStartDate());
            endDate.setText(list[position].getEndDate());

            return v;
        }
    }
}
