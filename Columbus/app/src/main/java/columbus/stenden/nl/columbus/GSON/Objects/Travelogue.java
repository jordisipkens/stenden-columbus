package columbus.stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 19/05/15.
 *
 *  Serializeble class for Travelogue (reisbeschrijving)
 */

public class Travelogue {
    @SerializedName("TravelogueID")
    public int logueId;
    @SerializedName("Note")
    public String note;
    @SerializedName("TravelID")
    public int travelId;

    public int getLogueId() {
        return logueId;
    }

    public void setLogueId(int logueId) {
        this.logueId = logueId;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }

    public int getTravelId() {
        return travelId;
    }

    public void setTravelId(int travelId) {
        this.travelId = travelId;
    }
}
