import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import { createTheme, ThemeProvider } from "@mui/material/styles";

// Define custom theme with softer shadows
const theme = createTheme({
  components: {
    MuiCard: {
      styleOverrides: {
        root: {
          boxShadow: "0px 4px 8px rgba(0, 0, 0, 0.1)", // Soft shadow
        },
      },
    },
  },
});

export default function Dashboard() {
  const [numAccounts, setNumAccounts] = useState(0);
  const [numTransactions, setNumTransactions] = useState(0);
  const [numDebitOrders, setNumDebitOrders] = useState(0);

  useEffect(() => {
    const accessToken = sessionStorage.getItem("accessToken");

    const getData = async () => {
      try {
        const response = await fetch( 
          `${process.env.REACT_APP_BASE_URL}/accounts/count`,
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${accessToken}`,
              "Content-Type": "application/json",
            },
          }
        );
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const results = await response.text();
        setNumAccounts(results);
      } catch (error) {}

      try {
        const response = await fetch(
          `${process.env.REACT_APP_BASE_URL}/accountTransactions/count`,
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${accessToken}`,
              "Content-Type": "application/json",
            },
          }
        );
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.text();

        setNumTransactions(data);
      } catch (error) {}

      try {
        const response = await fetch(
          `${process.env.REACT_APP_BASE_URL}/debitOrders`,
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${accessToken}`,
              "Content-Type": "application/json",
            },
          }
        );
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.json();
        setNumDebitOrders(data.length);
      } catch (error) {}
    };

    getData();
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <div className="p-8 flex flex-col items-start">
        <h1 className="text-2xl font-bold mb-4">Dashboard</h1>
        <hr className="w-full mb-4" />
        <Grid container spacing={3}>
          <Grid item xs={12} sm={6} md={4}>
            <Card>
              <CardContent>
                <Typography variant="h6" component="div">
                  <Link
                    to="/accounts"
                    className="text-green-800 hover:underline"
                  >
                    Accounts
                  </Link>
                </Typography>
                <Typography variant="h4" component="div" className="mt-2">
                  {numAccounts}
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12} sm={6} md={4}>
            <Card>
              <CardContent>
                <Typography variant="h6" component="div">
                  <Link
                    to="/debitorders"
                    className="text-green-800 hover:underline"
                  >
                    DebitOrders
                  </Link>
                </Typography>
                <Typography variant="h4" component="div" className="mt-2">
                  {numDebitOrders}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} sm={6} md={4}>
            <Card>
              <CardContent>
                <Typography variant="h6" component="div">
                  <Link
                    to="/transactions"
                    className="text-green-800 hover:underline"
                  >
                    Transactions
                  </Link>
                </Typography>
                <Typography variant="h4" component="div" className="mt-2">
                  {numTransactions}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      </div>
    </ThemeProvider>
  );
}
