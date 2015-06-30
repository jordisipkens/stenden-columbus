package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 18/05/15.
 *
 * Serializable class for the information about the travel.
 */
public class Travel {

    @SerializedName("ID")
    public int id;
    @SerializedName("Title")
    public String title;
    @SerializedName("StartDate")
    public String startDate;
    @SerializedName("EndDate")
    public String endDate;
    @SerializedName("Locations")
    public Location[] locations;
    @SerializedName("User")
    public User user;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getStartDate() {
        return startDate;
    }

    public void setStartDate(String startDate) {
        this.startDate = startDate;
    }

    public String getEndDate() {
        return endDate;
    }

    public void setEndDate(String endDate) {
        this.endDate = endDate;
    }

    public Location[] getLocations() {
        return locations;
    }

    public void setLocations(Location[] locations) {
        this.locations = locations;
    }

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }
}
