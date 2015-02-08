package com.example.beckyli.therostermobileapp;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.CalendarView;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.RadioGroup;
import android.widget.SeekBar;
import android.widget.Spinner;
import android.widget.Button;
import android.widget.ArrayAdapter;
import android.content.SharedPreferences;
import android.widget.TextView;


public class MainActivity extends ActionBarActivity {

    private EditText name;
    private CheckBox checkStable;
    private Spinner eyeColor;
    private CalendarView birthday;
    private RadioGroup rShirtSize;
    private SeekBar pantSize, sShirtSize, shoeSize;
    private TextView tSaved;
    private Button btnSubmit;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        initialization();
        addItemsOnSpinner();
        retrievePref();
    }


    protected void initialization(){

        name = (EditText) findViewById(R.id.editTextName);
        checkStable=(CheckBox) findViewById(R.id.checkBoxSteady);
        eyeColor = (Spinner) findViewById(R.id.spinnerEyeColor);
        birthday = (CalendarView) findViewById(R.id.calendarViewBirthday);
        rShirtSize = (RadioGroup) findViewById(R.id.radioGroupShirtSize);
        pantSize = (SeekBar) findViewById(R.id.seekBarPant);
        sShirtSize = (SeekBar) findViewById(R.id.seekBarShirt);
        shoeSize = (SeekBar) findViewById(R.id.seekBarShoe);
        tSaved = (TextView) findViewById(R.id.textViewSaved);

        pantSize.setOnSeekBarChangeListener(pantSizeListener);
        sShirtSize.setOnSeekBarChangeListener(sShirtSizeListener);
        shoeSize.setOnSeekBarChangeListener(shoeSizeListener);

    }


    SeekBar.OnSeekBarChangeListener  pantSizeListener = new SeekBar.OnSeekBarChangeListener() {

        @Override
        public void onStopTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }

        @Override
        public void onStartTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }

        @Override
        public void onProgressChanged(SeekBar seekBar, int progress,
                                      boolean fromUser) {
            // TODO Auto-generated method stub

            int iPantSize = progress / (100 / 16);

            TextView pantSize = (TextView) findViewById(R.id.pantSize);
            pantSize.setText("Pant Size: " + iPantSize);
        }
    };


    SeekBar.OnSeekBarChangeListener  sShirtSizeListener = new SeekBar.OnSeekBarChangeListener() {

        @Override
        public void onStopTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }

        @Override
        public void onStartTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }

        @Override
        public void onProgressChanged(SeekBar seekBar, int progress,
                                      boolean fromUser) {
            // TODO Auto-generated method stub

            int iShirtSize = progress / (100 / 12);

            TextView sShirtSize = (TextView) findViewById(R.id.sShirtSize);
            sShirtSize.setText("Shirt Size: " + iShirtSize);
        }
    };


    SeekBar.OnSeekBarChangeListener  shoeSizeListener = new SeekBar.OnSeekBarChangeListener() {

        @Override
        public void onStopTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }

        @Override
        public void onStartTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }


        @Override
        public void onProgressChanged(SeekBar seekBar, int progress,
                                      boolean fromUser) {
            // TODO Auto-generated method stub

            double dShoeSize = (progress +50)/12.5;

            TextView shoeSize = (TextView) findViewById(R.id.shoeSize);
            shoeSize.setText("Shoe Size: " + (int)dShoeSize);
        }
    };


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    public void addItemsOnSpinner(){

        eyeColor=(Spinner) findViewById(R.id.spinnerEyeColor);

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,R.array.eyeColor_array,android.R.layout.simple_spinner_dropdown_item);
        eyeColor.setAdapter(adapter);
    }


    public void retrievePref(){
        SharedPreferences  prefs = getSharedPreferences("", MODE_PRIVATE);

        name.setText(prefs.getString("name",""));
        checkStable.setChecked(prefs.getBoolean("checkStable",false));
        eyeColor.setSelection(prefs.getInt("eyeColor", 0));
        birthday.setDate(prefs.getLong("birthday",0));
        rShirtSize.check(prefs.getInt("rShirtSize",0));
        pantSize.setProgress(prefs.getInt("pantSize",0));
        sShirtSize.setProgress(prefs.getInt("sShirtSize",0));
        shoeSize.setProgress(prefs.getInt("shoeSize",0));

    }


    public void OnButtonClicked(View view){

        SharedPreferences  prefs = getSharedPreferences("", MODE_PRIVATE);
        SharedPreferences .Editor prefsEditor = prefs.edit();

        prefsEditor.putString("name",name.getText().toString());
        prefsEditor.putBoolean("checkStable", checkStable.isChecked());
        prefsEditor.putInt("eyeColor",eyeColor.getSelectedItemPosition());
        prefsEditor.putLong("birthday",birthday.getDate());
        prefsEditor.putInt("rShirtSize",rShirtSize.getCheckedRadioButtonId());
        prefsEditor.putInt("pantSize",pantSize.getProgress());
        prefsEditor.putInt("sShirtSize",sShirtSize.getProgress());
        prefsEditor.putInt("shoeSize",shoeSize.getProgress());
        prefsEditor.commit();

        tSaved.setText("Data Saved");
    }
}
