package stenden.nl.columbus.Fragments;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.GridView;

import java.util.ArrayList;
import java.util.List;

import stenden.nl.columbus.GSON.Objects.Paragraph;
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
           int size = travelogue.getParagraphs().length;

           for (int i = 0; i < size; i++){
               graphs.add(travelogue.getParagraphs()[i]);
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
            case R.id.travelogue_save:
                // Create array from the size of the childs of Gridview.
                Paragraph[] graphsArray = new Paragraph[grid.getCount()];

                // For loop to fill the array of Paragraphs.
                for(int i = 0; i < grid.getCount(); i++){
                    // Get EditText from the current child.
                    View view = grid.getChildAt(i);
                    EditText title = (EditText) view.findViewById(R.id.paragraph_title);
                    EditText text = (EditText) view.findViewById(R.id.paragraph_text);

                    // Create new Paragraph from the current child.
                    Paragraph graph = new Paragraph();
                    graph.setText(text.getText().toString());
                    graph.setHeader(title.getText().toString());

                    // Fill array on current index.
                    graphsArray[i] = graph;
                }
                if(graphsArray.length != 0) {
                    Travelogue logue = new Travelogue();
                    logue.setTravelId(travelID);
                    logue.setParagraphs(graphsArray);
                    if(MainActivity.travelogues.size() == 0){
                        Travelogue tTravelogue = new Travelogue();
                        tTravelogue.setId(travelID);
                        tTravelogue.setParagraphs(graphsArray);
                        MainActivity.travelogues.add(tTravelogue);
                    }
                    for(int j = 0; j < MainActivity.travelogues.size(); j++){
                        if( MainActivity.travelogues.get(j).getId() == travelID){
                            MainActivity.travelogues.set(j, logue);
                        } else {
                            Travelogue tLogue = new Travelogue();
                            tLogue.setId(travelID);
                            tLogue.setParagraphs(graphsArray);
                            MainActivity.travelogues.add(tLogue);
                        }
                    }
                }
                break;
            case R.id.add_paragraph:
                // Create arraylist from the size of the childs of Gridview.
                ArrayList<Paragraph> graphList = new ArrayList<>();

                // For loop to fill the array of Paragraphs.
                for(int i = 0; i < grid.getCount(); i++){
                    // Get EditText from the current child.
                    View view = grid.getChildAt(i);
                    EditText title = (EditText) view.findViewById(R.id.paragraph_title);
                    EditText text = (EditText) view.findViewById(R.id.paragraph_text);

                    // Create new Paragraph from the current child.
                    Paragraph graph = new Paragraph();
                    graph.setText(text.getText().toString());
                    graph.setHeader(title.getText().toString());

                    // Fill array on current index.
                    graphList.add(graph);
                }
                if(graphList.size() > 0){
                    ((ParagraphAdapter) grid.getAdapter()).addParagraph(graphList);
                }
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

            return v;
        }

        public void addParagraph(ArrayList<Paragraph> graphs){
            array = graphs;
            array.add(new Paragraph());
            ((BaseAdapter) grid.getAdapter()).notifyDataSetChanged();
        }

        public List<Paragraph> getParagraphs(){
            return array;
        }
    }
}
