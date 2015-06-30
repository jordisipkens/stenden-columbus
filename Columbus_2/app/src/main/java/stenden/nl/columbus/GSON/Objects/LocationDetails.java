package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 6/30/2015.
 */
public class LocationDetails{
    @SerializedName("ID")
    public int id;
    @SerializedName("Name")
    public String name;
    @SerializedName("Address")
    public String address;
    @SerializedName("PhoneNumber")
    public String phone;
    @SerializedName("PlaceID")
    public String placeID;
    @SerializedName("Coordinates")
    public Coordinates latlng;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getPlaceID() {
        return placeID;
    }

    public void setPlaceID(String placeID) {
        this.placeID = placeID;
    }

    public Coordinates getLatlng() {
        return latlng;
    }

    public void setLatlng(Coordinates latlng) {
        this.latlng = latlng;
    }


}