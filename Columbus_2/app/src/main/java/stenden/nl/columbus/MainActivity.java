package stenden.nl.columbus;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
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
import android.widget.BaseAdapter;
import android.widget.ListView;

import com.google.gson.Gson;

import stenden.nl.columbus.Fragments.AboutFragment;
import stenden.nl.columbus.Fragments.AccountFragment;
import stenden.nl.columbus.Fragments.HomeFragment;
import stenden.nl.columbus.Fragments.MapFragment;
import stenden.nl.columbus.GSON.Objects.LoginResponse;
import stenden.nl.columbus.GSON.Objects.User;


public class MainActivity extends ActionBarActivity
        implements NavigationDrawerFragment.NavigationDrawerCallbacks {

    /**
     * Fragment managing the behaviors, interactions and presentation of the navigation drawer.
     */
    private NavigationDrawerFragment mNavigationDrawerFragment;

    /**
     * Used to store the last screen title. For use in {@link #restoreActionBar()}.
     */
    private CharSequence mTitle;

    private Fragment mCurrentFragment;

    // Static variables used throughout fragments
    public static LoginResponse loginResponse = null;
    public final static String PREFS_NAME = "C0lumbus";
    public static User user = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        // Get SharedPreferences file.
        SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);

        // Get user logged in.
        user = new Gson().fromJson(settings.getString("user", null), User.class);

        // Get token and set loginresponse.
        loginResponse = new LoginResponse();
        loginResponse.setUser(user);
        loginResponse.setToken(settings.getString("token", null));

        if(loginResponse == null) {
            Intent intent = new Intent(this, LoginScreen.class);
            startActivity(intent);
        }
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mNavigationDrawerFragment = (NavigationDrawerFragment)
                getSupportFragmentManager().findFragmentById(R.id.navigation_drawer);
        mTitle = getTitle();

        // Set up the drawer.
        mNavigationDrawerFragment.setUp(
                R.id.navigation_drawer,
                (DrawerLayout) findViewById(R.id.drawer_layout));

        setBackStackListener();

        // Make the menu the desired color code.
        ActionBar bar = getSupportActionBar();
        bar.setDisplayShowHomeEnabled(false);
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#FF7200")));
    }

    @Override
    public void onBackPressed() {
        // When the current fragment is Home, pop all fragments until home fragment.
        if(getSupportFragmentManager().getBackStackEntryCount() >0){
            String currentBackStackLayer = getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName();
            if(currentBackStackLayer.equals("home")){
               System.exit(0);
            } else {
                super.onBackPressed();
            }
        } else {
            super.onBackPressed();
        }
    }

    @Override
    protected void onStop() {
        if(loginResponse != null){
            SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
            SharedPreferences.Editor editor = settings.edit();

            editor.putString("loginResponse", loginResponse.getToken()).commit();
            editor.putString("user", new Gson().toJson(user)).commit();
        }
        super.onStop();
    }

    /**
     * Make sure the backstack is in place so the fragments won't misplace.
     */
    private void setBackStackListener(){
        getSupportFragmentManager().addOnBackStackChangedListener(new FragmentManager.OnBackStackChangedListener() {
            @Override
            public void onBackStackChanged() {
                try {
                    String tag = getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName();
                    mCurrentFragment = (Fragment) getSupportFragmentManager().findFragmentByTag(tag);
                } catch (ArrayIndexOutOfBoundsException e){
                    System.exit(0);
                }
            }
        });
    }

    @Override
    public void onNavigationDrawerItemSelected(int position) {
        // update the main content by replacing fragments


        FragmentManager manager = getSupportFragmentManager();
        FragmentTransaction trans = manager.beginTransaction();
        Fragment newFragment = null;
        String tag = "";
        switch(position){
            // Home menu item.
            case 0:
                newFragment = new HomeFragment();
                tag = "home";
                break;
            // Account menu item.
            case 1:
                newFragment = new AccountFragment();
                tag = "account";
                break;
            // About menu item.
            case 2:
                newFragment = new AboutFragment();
                tag = "over_ons";
                break;
            case 3:
                newFragment = new MapFragment();
                tag = "map";
                break;
            case 4:
                loginResponse = null;
                user = null;
                SharedPreferences settings = getSharedPreferences(MainActivity.PREFS_NAME, 0);
                settings.edit().putString("loginResponse", null).commit();
                settings.edit().putString("user", null);
                startActivity(new Intent(this, LoginScreen.class));
                break;
        }
        if(mCurrentFragment == null){
            trans.replace(R.id.container, newFragment, tag);
            trans.addToBackStack(tag);
            trans.commit();
            //trans.hide(mCurrentFragment);
            mCurrentFragment = newFragment;
        }
        else if(newFragment != null) {
            if(newFragment.getClass() != mCurrentFragment.getClass()) {
                trans.replace(R.id.container, newFragment, tag);
                trans.addToBackStack("tag");
                trans.commit();
                trans.hide(mCurrentFragment);
                mCurrentFragment = newFragment;
            }
        }
    }

    /**
     * Make sure to add pages in the strings and here.
     * @param number
     */
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
            case 4:
                mTitle = getString(R.string.title_section4);
                break;
            case 5:
                mTitle = getString(R.string.title_section5);
                break;
        }
    }

    public void restoreActionBar() {
        ActionBar actionBar = getSupportActionBar();
        actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_STANDARD);
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

        return super.onOptionsItemSelected(item);
    }

    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {
        /**
         * The fragment argument representing the section number for this
         * fragment.
         */
        private static final String ARG_SECTION_NUMBER = "section_number";

        /**
         * Returns a new instance of this fragment for the given section
         * number.
         */
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
    }

}
