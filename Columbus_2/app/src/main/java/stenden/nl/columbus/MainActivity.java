package stenden.nl.columbus;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import com.google.gson.Gson;

import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import stenden.nl.columbus.Fragments.AboutFragment;
import stenden.nl.columbus.Fragments.AccountFragment;
import stenden.nl.columbus.Fragments.HomeFragment;
import stenden.nl.columbus.Fragments.MapFragment;
import stenden.nl.columbus.Fragments.NavigationDrawerFragment;
import stenden.nl.columbus.GSON.Objects.LoginResponse;
import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.GSON.Objects.Travelogue;
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

    /**
     * Make it available for NavigationDrawerFragment.
     * Used for menu.
     */
    public static Fragment mCurrentFragment;

    /**
     * Static variables that keep all the data retrieved from the API.
     */
    public static LoginResponse loginResponse = null;
    public final static String PREFS_NAME = "C0lumbus";
    public static User user = null;
    public static Travel[] travels = null;
    public static List<Travelogue> travelogues = null;
    public static Uri[] imageUris = null;

    // API URL
    public static String BASE_URL = "http://columbus-webservice.azurewebsites.net/api/";

    // GET requests
    public static String LOGIN_URL = "User/Login";
    public static String ALL_TRAVELS_URL = "Travel/GetAll";
    public static String ALL_TRAVELOGUE_URL = "Travelogue/GetAll";

    // POST requests
    private static String POST_TRAVELS_URL = "Travel";
    private static String POST_TRAVELOGUE_URL = "Travelogue";
    private static String POST_USER_URL = "User";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // Get SharedPreferences file.
        SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);

        // Get user logged in.
        user = new Gson().fromJson(settings.getString("user", null), User.class);
        travels = new Gson().fromJson(settings.getString("travels", null), Travel[].class);
        Travelogue[] logues = new Gson().fromJson(settings.getString("travelogues", null), Travelogue[].class);
        imageUris = new Gson().fromJson(settings.getString("uris", null), Uri[].class);

        if (logues != null) {
            travelogues = new ArrayList<Travelogue>();
            for (Travelogue x : logues) {
                travelogues.add(x);
            }
        }

        // Get token and set loginresponse.
        if (loginResponse == null) {
            loginResponse = new LoginResponse();
            loginResponse.setUser(user);
            loginResponse.setToken(settings.getString("loginResponse", null));
        }

        // if not logged in, go to login screen.
        if (loginResponse.getToken() == null) {
            Intent intent = new Intent(this, LoginScreen.class);
            startActivity(intent);
        }
        // Set content view.
        setContentView(R.layout.activity_main);

        // initiate DrawerFragment
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

    /**
     * Check which fragment is active. If Home is active, exit the application to the home screen.
     */
    @Override
    public void onBackPressed() {
        if (getSupportFragmentManager().getBackStackEntryCount() > 0) {
            String currentBackStackLayer = getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName();
            if (currentBackStackLayer.equals("home")) {
                Intent intent = new Intent(Intent.ACTION_MAIN);
                intent.addCategory(Intent.CATEGORY_HOME);
                intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                startActivity(intent);
            } else {
                super.onBackPressed();
            }
        } else {
            super.onBackPressed();
        }
    }

    /**
     * Save the necessary information in the sharedpreferences.
     */
    @Override
    protected void onStop() {
        if (loginResponse != null) {
            SharedPreferences settings = getSharedPreferences(PREFS_NAME, 0);
            SharedPreferences.Editor editor = settings.edit();

            editor.putString("loginResponse", loginResponse.getToken()).commit();
            editor.putString("user", new Gson().toJson(user)).commit();
            editor.putString("travels", new Gson().toJson(travels)).commit();
            editor.putString("uris", new Gson().toJson(imageUris)).commit();

            // revert the ArrayList to an Array for better serialisation.
            if (travelogues != null) {
                Travelogue[] logues = new Travelogue[travelogues.size()];
                for (int i = 0; i < travelogues.size(); i++) {
                    logues[i] = travelogues.get(i);
                }
                if (logues.length != 0) {
                    editor.putString("travelogues", new Gson().toJson(logues)).commit();
                }
            }
        }
        super.onStop();
    }

    @Override
    protected void onPause() {
        super.onPause();
    }

    @Override
    protected void onStart() {
        super.onStart();
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    /**
     * Make sure the backstack is in place so the fragments won't misplace.
     */
    private void setBackStackListener() {
        getSupportFragmentManager().addOnBackStackChangedListener(new FragmentManager.OnBackStackChangedListener() {
            @Override
            public void onBackStackChanged() {
                try {
                    String tag = getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName();
                    mCurrentFragment = (Fragment) getSupportFragmentManager().findFragmentByTag(tag);
                } catch (ArrayIndexOutOfBoundsException e) {
                    System.exit(0);
                }
            }
        });
    }

    /**
     * Make sure the right things happen when the menu items are selected.
     * @param position which is clicked on the menu
     */
    @Override
    public void onNavigationDrawerItemSelected(int position) {
        // update the main content by replacing fragments
        Fragment newFragment = null;
        String tag = "";
        switch (position) {
            // Home menu item.
            case 0:
                newFragment = new HomeFragment();
                tag = getString(R.string.title_section1);
                onNewFragment(newFragment, tag);
                break;
            // Account menu item.
            case 1:
                newFragment = new AccountFragment();
                tag = getString(R.string.title_section2);
                onNewFragment(newFragment, tag);
                break;
            // About menu item.
            case 2:
                newFragment = new AboutFragment();
                tag = getString(R.string.title_section3);
                onNewFragment(newFragment, tag);
                break;
            // Google Map item.
            case 3:
                newFragment = new MapFragment();
                tag = getString(R.string.title_section4);
                onNewFragment(newFragment, tag);
                break;
            // Log out item.
            case 4:
                loginResponse = null;
                user = null;
                travelogues = null;
                SharedPreferences settings = getSharedPreferences(MainActivity.PREFS_NAME, 0);
                settings.edit().putString("loginResponse", null).commit();
                settings.edit().putString("user", null).commit();
                settings.edit().putString("travels", null).commit();
                settings.edit().putString("travelogues", null).commit();
                startActivity(new Intent(this, LoginScreen.class));
                break;
            // Upload your date to database item.
            case 5:
                synchData();
                break;
        }
    }

    /**
     * 
     * @param newFragment the new fragment created and arguments set in the method @onNavigationDrawerItemSelected.
     * @param tag tag which will be added into the backstack
     */
    private void onNewFragment(Fragment newFragment, String tag) {

        if (mCurrentFragment == null) {
            getSupportFragmentManager().beginTransaction()
                    .replace(R.id.container, newFragment, tag)
                    .addToBackStack(tag).commit();

            mCurrentFragment = newFragment;
        } else if (newFragment != null) {
            if (newFragment.getClass() != mCurrentFragment.getClass()) {
                getSupportFragmentManager().beginTransaction()
                        .replace(R.id.container, newFragment, tag)
                        .addToBackStack(tag).hide(mCurrentFragment).commit();
                mCurrentFragment = newFragment;
            }
        }
    }

    /**
     * Make sure to add pages in the strings and here.
     *
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


    public void synchData() { // HTTP Request ombouwen
        //new UploadTravels().execute(BASE_URL + POST_TRAVELS_URL);
        //new UploadTravelogue().execute(BASE_URL + POST_TRAVELOGUE_URL);
        //new UploadUser().execute(BASE_URL + POST_USER_URL);
        Toast toast = new Toast(getApplicationContext()).makeText(getApplicationContext(), "Uploaden werkt momenteel niet door een bug. Zie het Nawoord in de scriptie voor meer informatie.", Toast.LENGTH_SHORT);
        toast.show();
    }

    /**
     * AsyncTask method to POST the travels to the webservice
     */
    class UploadTravels extends AsyncTask<String, Void, Void> {

        private Exception exception;

        protected Void doInBackground(String... urls) {
            for (Travel x : travels) {
                // Build the JSON object to pass parameters
                x.setUser(user);
                String json = new Gson().toJson(x);
                // Create the POST object and add the parameters
                try {
                    HttpPost httpPost = new HttpPost(urls[0]);
                    httpPost.setHeader("Token", loginResponse.getToken());
                    StringEntity entity = new StringEntity(json);
                    entity.setContentType("application/json");
                    httpPost.setEntity(entity);
                    HttpClient client = new DefaultHttpClient();
                    HttpResponse response = client.execute(httpPost);

                    BufferedReader reader = new BufferedReader(new InputStreamReader(response.getEntity().getContent(), "UTF-8"));
                    String something = reader.readLine();

                    Log.e("RESPONSE TRAVELOGE ", response.toString());
                } catch (IOException d) {
                    d.printStackTrace();
                }
            }
            return null;
        }

        protected void onPostExecute() {
        }
    }
    /**
     * AsyncTask method to POST the User to the webservice
     */
    class UploadUser extends AsyncTask<String, Void, Void> {

        private Exception exception;

        protected Void doInBackground(String... urls) {

            // Build the JSON object to pass parameters
            String json = new Gson().toJson(user);
            // Create the POST object and add the parameters
            try {
                HttpPost httpPost = new HttpPost(urls[0]);
                httpPost.setHeader("Token", loginResponse.getToken());
                StringEntity entity = new StringEntity(json);
                entity.setContentType("application/json");
                httpPost.setEntity(entity);
                HttpClient client = new DefaultHttpClient();
                HttpResponse response = client.execute(httpPost);

                BufferedReader reader = new BufferedReader(new InputStreamReader(response.getEntity().getContent(), "UTF-8"));
                String something = reader.readLine();

                Log.e("RESPONSE TRAVELOGE ", response.toString());
            } catch (IOException d) {
                d.printStackTrace();
            }
            return null;
        }

        protected void onPostExecute() {
        }
    }
    /**
     * AsyncTask method to POST the Travelogue to the webservice
     */
    class UploadTravelogue extends AsyncTask<String, Void, Void> {

        private Exception exception;

        protected Void doInBackground(String... urls) {
            for (Travelogue x : travelogues) {
                // Build the JSON object to pass parameters
                String json = new Gson().toJson(x);
                // Create the POST object and add the parameters
                try {
                    HttpPost httpPost = new HttpPost(urls[0]);
                    httpPost.setHeader("Token", loginResponse.getToken());
                    StringEntity entity = new StringEntity(json);
                    entity.setContentType("application/json");
                    httpPost.setEntity(entity);
                    HttpClient client = new DefaultHttpClient();
                    HttpResponse response = client.execute(httpPost);
                    BufferedReader reader = new BufferedReader(new InputStreamReader(response.getEntity().getContent(), "UTF-8"));
                    String something = reader.readLine();

                    Log.e("RESPONSE TRAVELOGE ", response.toString());
                } catch (IOException d) {
                    d.printStackTrace();
                }
            }
            return null;
        }

        protected void onPostExecute() {
            // TODO: check this.exception
            // TODO: do something with the feed
        }
    }
}
