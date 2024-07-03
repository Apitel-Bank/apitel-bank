import * as React from 'react';
import { Link } from 'react-router-dom';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import { createTheme, ThemeProvider } from '@mui/material/styles';

// Define custom theme with softer shadows
const theme = createTheme({
  components: {
    MuiCard: {
      styleOverrides: {
        root: {
          boxShadow: '0px 4px 8px rgba(0, 0, 0, 0.1)', // Soft shadow
        },
      },
    },
  },
});

export default function Dashboard() {
  // Dummy data for demonstration
  const data = {
    accounts: 10, 
    investments: 5, 
    payments: 20, 
    statements: 15, 
    transactions: 50, 
  };

  return (
    <ThemeProvider theme={theme}>
      <div className="p-8 flex flex-col items-start">
        <h1 className="text-2xl font-bold mb-4">Dashboard</h1>
        <hr className="w-full mb-4" />
        <div className='w-full bg-green-50 shadow-lg my-16 rounded-lg' >
          <h2>We have handled over 1.2B transactions</h2>
          

        </div>

        <Grid container spacing={3}>
          <Grid item xs={12} sm={6} md={4}>
            <Card>
              <CardContent>
                <Typography variant="h6" component="div">
                  <Link to="/accounts" className="text-green-800 hover:underline">
                    Accounts
                  </Link>
                </Typography>
                <Typography variant="h4" component="div" className="mt-2">
                  {data.accounts}
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12} sm={6} md={4}>
            <Card>
              <CardContent>
                <Typography variant="h6" component="div">
                  <Link to="/statements" className="text-green-800 hover:underline">
                    Statements
                  </Link>
                </Typography>
                <Typography variant="h4" component="div" className="mt-2">
                  {data.statements}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} sm={6} md={4}>
            <Card>
              <CardContent>
                <Typography variant="h6" component="div">
                  <Link to="/transactions" className="text-green-800 hover:underline">
                    Transactions
                  </Link>
                </Typography>
                <Typography variant="h4" component="div" className="mt-2">
                  {data.transactions}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      </div>
    </ThemeProvider>
  );
}
