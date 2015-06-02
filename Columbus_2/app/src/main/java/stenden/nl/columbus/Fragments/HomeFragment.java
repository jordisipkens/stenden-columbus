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

import java.util.ArrayList;

import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 19/05/15.
 *
 * Fragment for the home screen of the user.
 * Should display travels when they are bounded to the current account.
 */
public class HomeFragment extends Fragment {

    private ListView mTravelList;
    private TravelAdapter mAdapter;
    private Travel[] travels;

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

        // Create test array[] of Travel objects
        ArrayList<Travel> arrayList = new ArrayList(5);
        for(int i = 0; i < 5; i++){
            Travel object = new Travel();
            object.setStartDate(i + "-03-2015");
            object.setEndDate(i + "-05-2015");

            arrayList.add(i, object);
        }
        travels = new Travel[]{arrayList.get(0), arrayList.get(1), arrayList.get(2), arrayList.get(3), arrayList.get(4)};


        mAdapter = new TravelAdapter(travels, getActivity());
        mTravelList.setAdapter(mAdapter);

        setAdapterListener();

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

    private void setAdapterListener(){
        mTravelList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                // new fragment with travel details.
                FragmentManager manager = getActivity().getSupportFragmentManager();
                FragmentTransaction trans = manager.beginTransaction();

                trans.add(R.id.container, new TravelDetailFragment(), "Detail");
                trans.addToBackStack("Detail");
                trans.commit();
            }
        });
    }

    private class TravelAdapter extends BaseAdapter{
        private Travel[] list;
        private Context ctx;
        private View v;
        private TextView startDate, endDate;

        public TravelAdapter(Travel[] list, Context ctx){
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
