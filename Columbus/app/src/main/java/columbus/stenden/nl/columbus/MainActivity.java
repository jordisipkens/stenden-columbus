package columbus.stenden.nl.columbus;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;

import columbus.stenden.nl.columbus.Fragments.AboutFragment;
import columbus.stenden.nl.columbus.Fragments.AccountFragment;
import columbus.stenden.nl.columbus.Fragments.HomeFragment;
import columbus.stenden.nl.columbus.Fragments.MapFragment;


public class MainActivity extends ActionBarActivity
        implements NavigationDrawerFragment.NavigationDrawerCallbacks {

    /**
     * Fragment managing the behaviors, interactions and presentation of the navigation drawer.
     */
    private NavigationDrawerFragment mNavigationDrawerFragment;

    /**
     * ListView managing the navigation.
     */
    private ListView mDrawerList;

    /**
     * The drawer itself, needed to make it close when clicked on etc.
     */
    private DrawerLayout mDrawer;

    /**
     * Global variable of the current fragment showing, needed for better control of fragments.
     */
    private Fragment mCurrentFragment;

    /**
     * two global variables needed to create and view new and old fragments.
     */
    private FragmentManager manager;
    private FragmentTransaction trans;

    /**
     * Variable needed to see which item menu is already clicked so it can't be clicked twice.\
     *
     * Starts at 0 just like HomeFragment
     *
     * !! If the initial fragment changes or changes in the array, make sure to change this too!
     */
    private int itemClicked = 0;

    /**
     * The array with the menu items.
     *
     * !! - Make sure to add the new items to the onItemClickListener of the ListView.
     */
    private String[] menuTitles = new String[]{"Home", "Account", "Over", "Google Map"};

    /**
     * Used to store the last screen title. For use in {@link #restoreActionBar()}.
     */
    private CharSequence mTitle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mNavigationDrawerFragment = (NavigationDrawerFragment)
                getSupportFragmentManager().findFragmentById(R.id.navigation_drawer);
        mTitle = getTitle();

        mDrawer = (DrawerLayout) findViewById(R.id.drawer_layout);

        mDrawerList = (ListView) findViewById(R.id.drawer_list);

        // Set up the drawer.
        mNavigationDrawerFragment.setUp(
                R.id.navigation_drawer,
                mDrawer);

        //set FragmentManager and set the first fragment (HomeFragment).
        manager = getSupportFragmentManager();
        trans = manager.beginTransaction();
        mCurrentFragment = new HomeFragment();
        trans.replace(R.id.container, mCurrentFragment, "Home");
        trans.commit();

        //Set the menu ListView
        setDrawerListView();

        // Make the menu the desired color code.
        ActionBar bar = getSupportActionBar();
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#FF7200")));
    }

    @Override
    public void onNavigationDrawerItemSelected(int position) {
        // update the main content by replacing fragments
        // FragmentManager fragmentManager = getSupportFragmentManager();
    }

    public void onSectionAttached(int number) {
        switch (number) {
            case 1:
                mTitle = getString(R.string.title_section1);
                break;
            case 2:
                mTitle = getString(R.string.title_section2);
                break;
            case 3:
                mTitle = getString(R.string.title_section3);
                break;
        }
    }

    public void restoreActionBar() {
        ActionBar actionBar = getSupportActionBar();
        //actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_STANDARD);
        actionBar.setDisplayShowTitleEnabled(true);
        actionBar.setTitle(mTitle);
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (!mNavigationDrawerFragment.isDrawerOpen()) {
            // Only show items in the action bar relevant to this screen
            // if the drawer is not showing. Otherwise, let the drawer
            // decide what to show in the action bar.
            getMenuInflater().inflate(R.menu.main, menu);
            restoreActionBar();
            return true;
        }
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    /*/**
     * A placeholder fragment containing a simple view.
     *
    public static class PlaceholderFragment extends Fragment {
        /**
         * The fragment argument representing the section number for this
         * fragment.
         *
        private static final String ARG_SECTION_NUMBER = "section_number";

        /**
         * Returns a new instance of this fragment for the given section
         * number.
         *
        public static PlaceholderFragment newInstance(int sectionNumber) {
            PlaceholderFragment fragment = new PlaceholderFragment();
            Bundle args = new Bundle();
            args.putInt(ARG_SECTION_NUMBER, sectionNumber);
            fragment.setArguments(args);
            return fragment;
        }

        public PlaceholderFragment() {
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {
            View rootView = inflater.inflate(R.layout.fragment_main, container, false);
            return rootView;
        }

        @Override
        public void onAttach(Activity activity) {
            super.onAttach(activity);
            ((MainActivity) activity).onSectionAttached(
                    getArguments().getInt(ARG_SECTION_NUMBER));
        }
    }*/

    private void setDrawerListView(){
        if(mDrawerList != null) {
            mDrawerList.setAdapter(new MenuAdapter(this, menuTitles));
            setOnClickListener();
        } else {

        }
    }

    private void setOnClickListener(){
        mDrawerList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                itemClicked = position;
                mTitle = menuTitles[position];
                FragmentManager manager = getSupportFragmentManager();
                FragmentTransaction trans = manager.beginTransaction();
                Fragment newFragment = null;
                String tag;
                switch(position){
                    // Home menu item.
                    case 0:
                        newFragment = new HomeFragment();
                        tag = "home";
                        break;
                    // Account menu item.
                    case 1:
                        newFragment = new AccountFragment();
                        tag = "home";
                        break;
                    // About menu item.
                    case 2:
                        newFragment = new AboutFragment();
                        tag = "home";
                        break;
                    case 3:
                        newFragment = new MapFragment();
                        tag = "map";
                        break;
                }
                if(newFragment != null) {
                    if(newFragment.getClass() != mCurrentFragment.getClass()) {
                        trans.replace(R.id.container, newFragment, "Home");
                        trans.commit();
                        trans.hide(mCurrentFragment);
                        mCurrentFragment = newFragment;
                        mDrawer.closeDrawer(mNavigationDrawerFragment.getView());
                    }
                }
                // Make sure the menu refreshes so the correct item is highlighted.
                ((BaseAdapter) mDrawerList.getAdapter()).notifyDataSetChanged();
            }
        });
    }

    private class MenuAdapter extends BaseAdapter{

        // Global view returns the menu item.
        private View v;
        private Context ctx;
        private TextView title;
        private String[] titles;

        public MenuAdapter(Context ctx, String[] titles){
            this.ctx = ctx;
            this.titles = titles;
        }
        @Override
        public int getCount() {
            return titles.length;
        }

        @Override
        public Object getItem(int position) {
            return titles[position];
        }

        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            v = LayoutInflater.from(ctx).inflate(R.layout.menu_list_item,
                    parent, false);
            title = (TextView) v.findViewById(R.id.menu_title);

            title.setText(titles[position]);

            if(!isEnabled(position)){
                v.setBackgroundColor(Color.parseColor("#ff9c4d"));
            }

            return v;
        }

        @Override
        public boolean isEnabled(int position) {
            if(itemClicked == position){
                return false;
            } else {
                return true;
            }
        }
    }

}
