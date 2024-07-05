package com.apitel_bank.main_banking_service.services;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;

import org.json.JSONObject;
import org.springframework.stereotype.Service;

@Service
public class BusinessRegisterService {

    String taxId;

    public void registerWithSARS() {
        try {
            @SuppressWarnings("deprecation")
            URL url = new URL("https://api.mers.projects.bbdgrad.com/api/taxpayer/business/register");
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/json");
            connection.setRequestProperty("X-Origin", "retail_bank");
            connection.setDoOutput(true);

            String jsonInputString = "{\"businessName\": \"ApitelRetailBank\"}";

            try (OutputStream os = connection.getOutputStream()) {
                byte[] input = jsonInputString.getBytes(StandardCharsets.UTF_8);
                os.write(input, 0, input.length);
            }

            int code = connection.getResponseCode();
            System.out.println("HELLO" + code);
            if (code == HttpURLConnection.HTTP_OK) {

                try (BufferedReader br = new BufferedReader(
                        new InputStreamReader(connection.getInputStream(), StandardCharsets.UTF_8))) {
                    StringBuilder response = new StringBuilder();
                    String responseLine;
                    while ((responseLine = br.readLine()) != null) {
                        response.append(responseLine.trim());
                    }

                    // Parse the JSON response
                    JSONObject jsonResponse = new JSONObject(response.toString());
                    String identifier = jsonResponse.getString("taxId");
                    taxId = identifier;
                }
            } else {
                System.out.println("API call failed with response code: " + code);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
}
