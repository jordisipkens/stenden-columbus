package stenden.nl.columbus.Fragments;

import android.content.Context;
import android.hardware.GeomagneticField;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.List;

import stenden.nl.columbus.MainActivity;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 20/05/15.
 */
public class MapFragment extends Fragment {
    private GoogleMap mGoogleMap;
    private float mDeclination;
    private double[] coordinates;
    private String locTitle;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        try {
            coordinates = getArguments().getDoubleArray("coordinates");
            locTitle = getArguments().getString("locTitle");
            // For when it is not set.
        } catch (NullPointerException e){

        }
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_map, container, false);

        initMap();

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

    public void initMap() {
        final SupportMapFragment myMAPF = (SupportMapFragment) getChildFragmentManager()
                .findFragmentById(R.id.mapView);
        myMAPF.getMapAsync(new OnMapReadyCallback() {
            @Override
            public void onMapReady(GoogleMap googleMap) {
                mGoogleMap = googleMap;

                mGoogleMap.setPadding(50, 0, 0, 0);
                mGoogleMap.getUiSettings().setMyLocationButtonEnabled(false);
                mGoogleMap.setMyLocationEnabled(true);
                //mGoogleMap.setOnInfoWindowClickListener(MapFragment.this);
                mGoogleMap
                        .setOnMyLocationChangeListener(new GoogleMap.OnMyLocationChangeListener() {

                            @Override
                            public void onMyLocationChange(Location location) {
                                // TODO Auto-generated method stub
                                GeomagneticField field = new GeomagneticField(
                                        (float) location.getLatitude(),
                                        (float) location.getLongitude(),
                                        (float) location.getAltitude(), System
                                        .currentTimeMillis());

                                // getDeclination returns degrees
                                mDeclination = field.getDeclination();
                            }

                        });

                if(coordinates != null){
                    mGoogleMap.addMarker(new MarkerOptions()
                            .position(new LatLng(coordinates[0], coordinates[1]))
                            .title(locTitle));

                    CameraPosition cameraPosition = new CameraPosition.Builder()
                            .target(new LatLng(coordinates[0], coordinates[1]))      // Sets the center of the map to location user
                            .zoom(12)                   // Sets the zoom
                            .bearing(90)                // Sets the orientation of the camera to east
                            .tilt(0)                   // Sets the tilt of the camera to 30 degrees
                            .build();                   // Creates a CameraPosition from the builder
                    mGoogleMap.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition));
                } else {
                    moveMapToCurrentLocation();
                }
            }
        });
    }

    private void moveMapToCurrentLocation(){
        LocationManager locationManager = (LocationManager) getActivity().getSystemService(Context.LOCATION_SERVICE);
        Criteria criteria = new Criteria();

        Location location = locationManager.getLastKnownLocation(locationManager.getBestProvider(criteria, false));
        if (location != null)
        {

            CameraPosition cameraPosition = new CameraPosition.Builder()
                    .target(new LatLng(location.getLatitude(), location.getLongitude()))      // Sets the center of the map to location user
                    .zoom(10)                   // Sets the zoom
                    .bearing(90)                // Sets the orientation of the camera to east
                    .tilt(0)                   // Sets the tilt of the camera to 30 degrees
                    .build();                   // Creates a CameraPosition from the builder
            mGoogleMap.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition));

        }
    }
}
