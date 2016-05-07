package com.lucamollo.magnetometerandroid;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.hardware.SensorEvent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity implements SensorEventListener {

    private SensorManager mSensorManager;
    private Sensor magnetometer;

    private TextView mxValue;
    private TextView myValue;
    private TextView mzValue;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mxValue = (TextView)findViewById(R.id.x_value);
        myValue = (TextView)findViewById(R.id.y_value);
        mzValue = (TextView)findViewById(R.id.z_value);

        mSensorManager = (SensorManager)getSystemService(Context.SENSOR_SERVICE);
        magnetometer = mSensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD);

        if(magnetometer != null)
        {
            //In microsecondi. 0 se ritorna un valore solo quando cambia il dato
            int minDelay = magnetometer.getMinDelay();

        }
        else
        {

        }
    }

    @Override
    public final void onSensorChanged(SensorEvent event) {
        mxValue.setText(Float.toString(event.values[0]));
        myValue.setText(Float.toString(event.values[1]));
        mzValue.setText(Float.toString(event.values[2]));
    }

    @Override
    public final void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    @Override
    protected void onResume() {
        super.onResume();
        mSensorManager.registerListener(this, magnetometer, SensorManager.SENSOR_DELAY_NORMAL);
    }

    @Override
    protected void onPause() {
        super.onPause();
        mSensorManager.unregisterListener(this);
    }

}
