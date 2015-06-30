package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 18/05/15.
 *
 * Serializeble class for the information about a location that is included in the Travel object.
 */
public class Location {
    @SerializedName("ID")
    public int id;
    @SerializedName("Date")
    public String date;
    @SerializedName("Note")
    public String note;
    @SerializedName("LocationDetails")
    public LocationDetails locDetails;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }

    public LocationDetails getLocationDetails() {
        return locDetails;
    }

    public void setLocationDetails(LocationDetails locationDetails) {
        this.locDetails = locationDetails;
    }


}
