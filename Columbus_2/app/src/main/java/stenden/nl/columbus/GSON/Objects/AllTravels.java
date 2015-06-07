package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 07/06/15.
 *
 * Object which is made after retrieving all travels from user.
 */
public class AllTravels {
    @SerializedName("Travels")
    public Travel[] travels;

    public Travel[] getTravels() {
        return travels;
    }

    public void setTravels(Travel[] travels) {
        this.travels = travels;
    }
}
