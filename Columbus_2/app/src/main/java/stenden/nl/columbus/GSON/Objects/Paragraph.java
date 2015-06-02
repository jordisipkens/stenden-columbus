package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 19/05/15.
 *
 * Serializeble class for paragraphs for making the travelogue.
 */
public class Paragraph {

    @SerializedName("ID")
    public int id;
    @SerializedName("Header")
    public String header;
    @SerializedName("Text")
    public String text;
    @SerializedName("PhotoID")
    public int photoId;
    @SerializedName("FullWidth")
    public boolean fullWidth;
    @SerializedName("Width")
    public int width;
    @SerializedName("height")
    public int height;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getHeader() {
        return header;
    }

    public void setHeader(String header) {
        this.header = header;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public int getPhotoId() {
        return photoId;
    }

    public void setPhotoId(int photoId) {
        this.photoId = photoId;
    }

    public boolean isFullWidth() {
        return fullWidth;
    }

    public void setFullWidth(boolean fullWidth) {
        this.fullWidth = fullWidth;
    }

    public int getWidth() {
        return width;
    }

    public void setWidth(int width) {
        this.width = width;
    }

    public int getHeight() {
        return height;
    }

    public void setHeight(int height) {
        this.height = height;
    }
}
