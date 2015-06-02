package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 18/05/15.
 *
 * Serializeble class for the information about a location that is included in the TravelIn he.
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
    }
}
