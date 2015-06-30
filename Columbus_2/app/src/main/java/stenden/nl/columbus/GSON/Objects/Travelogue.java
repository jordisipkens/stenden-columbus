package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 19/05/15.
 *
 *  Serializable class for Travelogue (Reisverslag)
 */

public class Travelogue {
    @SerializedName("ID")
    public int id;
    @SerializedName("TravelID")
    public int travelId;
    @SerializedName("Published")
    public boolean published;
    @SerializedName("Paragraphs")
    public Paragraph[] paragraphs;
    @SerializedName("Ratings")
    public Rating[] ratings;

    public int getLogueId() {
        return id;
    }

    public void setLogueId(int logueId) {
        this.id = logueId;
    }

    public int getTravelId() {
        return travelId;
    }

    public void setTravelId(int travelId) {
        this.travelId = travelId;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public boolean isPublished() {
        return published;
    }

    public void setPublished(boolean published) {
        this.published = published;
    }

    public Paragraph[] getParagraphs() {
        return paragraphs;
    }

    public void setParagraphs(Paragraph[] paragraphs) {
        this.paragraphs = paragraphs;
    }

    public Rating[] getRatings() {
        return ratings;
    }

    public void setRatings(Rating[] ratings) {
        this.ratings = ratings;
    }

    public class Rating{
        @SerializedName("ID")
        public int id;
        @SerializedName("RatingValue")
        public double ratingValue;

        public int getId() {
            return id;
        }

        public void setId(int id) {
            this.id = id;
        }

        public double getRatingValue() {
            return ratingValue;
        }

        public void setRatingValue(double ratingValue) {
            this.ratingValue = ratingValue;
        }
    }
}
