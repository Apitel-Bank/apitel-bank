import {
  Button,
  Card,
  CardContent,
  TextField,
  Typography,
  Box,
} from "@mui/material";
import "./App.css";
import NavBar from "./components/navbar/navbar";
import Accounts from "./routes/accounts/accounts";
import Dashboard from "./routes/dashboard/dashbord";
import Investments from "./routes/investments/investments";
import Payments from "./routes/payments/payments";
import Settings from "./routes/settings/settings";
import Statements from "./routes/statements/statements";
import Transactions from "./routes/transactions/transactions";
import {
  BrowserRouter as Router,
  Routes, Route, Navigate,
} from "react-router-dom";
import { useState } from "react";
import userPool from "./shared/userPool";
import { AuthenticationDetails, CognitoUser } from "amazon-cognito-identity-js";

function App() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [loginError, setLoginError] = useState("");

  function handleSubmitButton(event) {
    event.preventDefault();

    const user = new CognitoUser({
      Username: username,
      Pool: userPool,
    });

    const authDetails = new AuthenticationDetails({
      Username: username,
      Password: password,
    });

    user.authenticateUser(authDetails, {
      onSuccess: (data) => {
        console.log("OnSuccess: ", data);
        setIsLoggedIn(true); // Set login state to true
      },
      onFailure: (err) => {
        console.log("OnFailure: ", err);
        setLoginError("Login failed. Please check your username and password.");
      },
      newPasswordRequired: (data) => {
        console.log("NewPasswordRequired: ", data);
      },
    });
  }

  if (!isLoggedIn) {
    return (
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          minHeight: '100vh',
          backgroundColor: '#f0f2f5',
          padding: '1rem', // Add padding for mobile responsiveness
        }}
      >
        <Card
          sx={{
            width: '100%', // Make card take full width on mobile
            maxWidth: 400, // Limit max width for larger screens
            padding: '2rem',
            borderRadius: '1rem',
            boxShadow: '0 4px 10px rgba(0, 0, 0, 0.1)',
            display: 'flex',
            flexDirection: 'column',
            gap: '1.5rem',
          }}
        >
          <CardContent>
            <Typography
              variant="h4"
              sx={{
                marginBottom: '1rem',
                fontWeight: 'bold',
                textAlign: 'center',
                color: '#3f51b5',
              }}
            >
              Login
            </Typography>
            <form onSubmit={handleSubmitButton}>
              <TextField
                id="username"
                label="Username"
                variant="outlined"
                type="email"
                value={username}
                onChange={(event) => setUsername(event.target.value)}
                fullWidth
                sx={{ marginBottom: '1rem' }}
              />
              <TextField
                id="password"
                label="Password"
                variant="outlined"
                type="password"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
                fullWidth
                sx={{ marginBottom: '1rem' }}
              />
              {loginError && (
                <Typography sx={{ color: 'red', textAlign: 'center', marginTop: '0.5rem' }}>
                  {loginError}
                </Typography>
              )}
              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
                sx={{ marginTop: '1rem' }}
              >
                Submit
              </Button>
            </form>
          </CardContent>
        </Card>
      </Box>
    );
  }

  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/accounts" element={<Accounts />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/investments" element={<Investments />} />
        <Route path="/payments" element={<Payments />} />
        <Route path="/settings" element={<Settings />} />
        <Route path="/statements" element={<Statements />} />
        <Route path="/transactions" element={<Transactions />} />
        <Route path="*" element={<Navigate to="/dashboard" />} />
      </Routes>
    </Router>
  );
}

export default App;
