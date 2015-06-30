package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 6/30/2015.
 */
public class Coordinates{

    @SerializedName("Latitude")
    public double lat;
    @SerializedName("Longitude")
    public double lng;

    public double getLat() {
        return lat;
    }

    public void setLat(double lat) {
        this.lat = lat;
    }

    public double getLng() {
        return lng;
    }

    public void setLng(double lng) {
        this.lng = lng;
    }
}
