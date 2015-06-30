package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 6/2/2015.
 *
 * Serializable class for sending and retrieving Photos from the webservice.
 *
 * Not used.
 */
public class Photo {
    @SerializedName("ID")
    public int id;
    @SerializedName("Caption")
    public String caption;
    @SerializedName("URL")
    public String url;
    @SerializedName("LocationId")
    public int locationId;
    @SerializedName("TravelId")
    public int travelId;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getCaption() {
        return caption;
    }

    public void setCaption(String caption) {
        this.caption = caption;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public int getLocationId() {
        return locationId;
    }

    public void setLocationId(int locationId) {
        this.locationId = locationId;
    }

    public int getTravelId() {
        return travelId;
    }

    public void setTravelId(int travelId) {
        this.travelId = travelId;
    }
}
