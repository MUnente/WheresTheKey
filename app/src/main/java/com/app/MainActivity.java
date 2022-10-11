package com.app;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        EditText txtUserId = (EditText) findViewById(R.id.txtUserId);
        Button btnEnter = (Button) findViewById(R.id.btnEnter);

        btnEnter.setOnClickListener((View view) -> {
            Toast.makeText(this, "Hello, World!", Toast.LENGTH_SHORT).show();
        });
    }
}