package stenden.nl.columbus.GSON.Objects;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Jordi on 18/05/15.
 *
 * Serializeble class for the information about the user.
 */
public class User {
    @SerializedName("ID")
    public int id;
    @SerializedName("Email")
    public String email;
    @SerializedName("FirstName")
    public String name;
    @SerializedName("LastName")
    public String lastName;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }
}
