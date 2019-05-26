package com.mycompany.myapp;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;

public class HttpSendJSON不好用 {
    /**
     * JSON文字列の送信
     * @param strPostUrl 送信先URL
     * @param JSON 送信するJSON文字列
     * @return     */
    public String callPost(String strPostUrl, String JSON) {

        HttpURLConnection con = null;
        StringBuffer result = new StringBuffer();
        try {

            URL url = new URL(strPostUrl);
            con = (HttpURLConnection) url.openConnection();
            // HTTPリクエストコード
            con.setDoOutput(true);
            con.setRequestMethod("POST");
            con.setRequestProperty("Accept-Language", "jp");
            // データがJSONであること、エンコードを指定する
            con.setRequestProperty("Content-Type", "application/JSON; charset=utf-8");
            // POSTデータの長さを設定
            con.setRequestProperty("Content-Length", String.valueOf(JSON.length()));
            // リクエストのbodyにJSON文字列を書き込む
            OutputStreamWriter out = new OutputStreamWriter(con.getOutputStream());
            out.write(JSON);
            out.flush();
            con.connect();

            // HTTPレスポンスコード
            final int status = con.getResponseCode();
            if (status == HttpURLConnection.HTTP_OK) {
                // 通信に成功した
                // テキストを取得する
                final InputStream in = con.getInputStream();
                String encoding = con.getContentEncoding();
                if (null == encoding) {
                    encoding = "UTF-8";
                }
                final InputStreamReader inReader = new InputStreamReader(in, encoding);
                final BufferedReader bufReader = new BufferedReader(inReader);
                String line = null;
                // 1行ずつテキストを読み込む
                while ((line = bufReader.readLine()) != null) {
                    result.append(line);
                }
                bufReader.close();
                inReader.close();
                in.close();
            } else {
                // 通信が失敗した場合のレスポンスコードを表示
                System.out.println(status);
            }

        } catch (Exception e1) {
            e1.printStackTrace();
        } finally {
            if (con != null) {
                // コネクションを切断
                con.disconnect();
            }
        }
        return result.toString();
    }
}