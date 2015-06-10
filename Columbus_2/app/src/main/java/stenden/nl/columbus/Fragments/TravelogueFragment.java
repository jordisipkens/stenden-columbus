package stenden.nl.columbus.Fragments;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.GridLayout;
import android.widget.GridView;
import android.widget.TextView;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import stenden.nl.columbus.GSON.Objects.Paragraph;
import stenden.nl.columbus.GSON.Objects.Travel;
import stenden.nl.columbus.GSON.Objects.Travelogue;
import stenden.nl.columbus.MainActivity;
import stenden.nl.columbus.R;

/**
 * Created by Jordi on 6/10/2015.
 */
public class TravelogueFragment extends Fragment implements View.OnClickListener{

    private GridView grid;
    private Button cancel, save, pNew;
    private int travelID;
    private Travelogue travelogue;

    public TravelogueFragment() {
        super();
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        travelID = getArguments().getInt("travelID");

        for(Travelogue x: MainActivity.travelogues){
            if(travelID == x.getTravelId()){
                travelogue = x;
            }
        }

        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_travelogue, container, false);

        cancel = (Button) v.findViewById(R.id.cancel);
        save = (Button) v.findViewById(R.id.travelogue_save);
        pNew = (Button) v.findViewById(R.id.add_paragraph);

        cancel.setOnClickListener(this);
        save.setOnClickListener(this);
        pNew.setOnClickListener(this);

        grid = (GridView) v.findViewById(R.id.travelogue_grid);

        ArrayList<Paragraph> graphs = new ArrayList<>();
       try {
           for (Paragraph x : travelogue.getParagraphs()) {
               graphs.add(x);
           }
       } catch( NullPointerException e){
           e.printStackTrace();
       }
        if(graphs.size() == 0){
            graphs.add(new Paragraph());
        }

        grid.setAdapter(new ParagraphAdapter(graphs, getActivity()));

        return v;
    }

    @Override
    public void onStart() {
        super.onStart();
    }

    @Override
    public void onResume() {
        super.onResume();
    }

    @Override
    public void onPause() {
        super.onPause();
    }

    @Override
    public void onStop() {
        super.onStop();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
    }

    @Override
    public void onClick(View v) {
        switch(v.getId()){
            case R.id.cancel:
                getActivity().getSupportFragmentManager().popBackStack();
                break;
            case R.id.submit:
                List<Paragraph> graphs = ((ParagraphAdapter) grid.getAdapter()).getParagraphs();
                Paragraph[] graphsArray = new Paragraph[graphs.size()];
                for(int i = 0 ; i < graphs.size(); i++){
                    graphsArray[i] = graphs.get(i);
                }
                if(graphsArray.length != 0) {
                    travelogue.setTravelId(travelID);
                    travelogue.setParagraphs(graphsArray);
                    for(Travelogue x: MainActivity.travelogues){
                        if(x.getId() == travelID){
                            x = travelogue;
                        }
                    }
                }
                break;
            case R.id.add_paragraph:
                ((ParagraphAdapter) grid.getAdapter()).addParagraph();
                break;
        }
    }

    private class ParagraphAdapter extends BaseAdapter{

        private Context ctx;
        private View v;
        private EditText pTitle, pText;
        private List<Paragraph> array;

        public ParagraphAdapter(ArrayList<Paragraph> array, Context ctx) {
            this.array = array;
            this.ctx = ctx;
        }

        @Override
        public int getCount() {
            return array.size();
        }

        @Override
        public Object getItem(int i) {
            return array.get(i);
        }

        @Override
        public long getItemId(int i) {
            return array.get(i).getId();
        }

        @Override
        public View getView(final int position, View view, ViewGroup parent) {
            v = LayoutInflater.from(ctx).inflate(R.layout.travelogue_paragraph,
                    parent, false);

            pTitle = (EditText) v.findViewById(R.id.paragraph_title);
            pText = (EditText) v.findViewById(R.id.paragraph_text);

            Paragraph graph = array.get(position);

            if(graph.getHeader() != null){
                pTitle.setText(graph.getHeader());
            }

            if(graph.getText() != null){
                pText.setText(graph.getText());
            }

            pTitle.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }

                @Override
                public void afterTextChanged(Editable s) {
                    Paragraph graph = new Paragraph();
                    graph.setHeader(s.toString());
                    graph.setText(pText.getText().toString());

                    array.set(position, graph);
                }
            });

            pText.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }

                @Override
                public void afterTextChanged(Editable s) {

                    Paragraph graph = new Paragraph();
                    graph.setHeader(pTitle.getText().toString());
                    graph.setText(s.toString());

                    array.set(position, graph);
                }
            });

            return v;
        }

        public void addParagraph(){
            array.add(new Paragraph());
            ((BaseAdapter) grid.getAdapter()).notifyDataSetChanged();
        }

        public List<Paragraph> getParagraphs(){
            return array;
        }
    }
}
