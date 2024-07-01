import { Button, Card, CardContent, Input, TextField } from "@mui/material";
import "./App.css";
import NavBar from "./components/navbar/navbar";
import Accounts from "./routes/accounts/accounts";
import Dashboard from "./routes/dashboard/dashbord";
import Investments from "./routes/investments/investments";
import Payments from "./routes/payments/payments";
import Settings from "./routes/settings/settings";
import Statements from "./routes/statements/statements";
import Transactions from "./routes/transactions/transactions";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useState } from "react";
import userPool from "./shared/userPool";
import { AuthenticationDetails, CognitoUser } from "amazon-cognito-identity-js";

function App() {
  const [
    username, 
    setUsername
  ] = useState('');

  const [
    password, 
    setPassword
  ] = useState('');

  function handleSubmitButton(event) {
    console.log(userPool);

    // userPool.signUp(username, password, [], null, (err, data) => {
    //   if (err) console.log(err);
    //   console.log(data);
    // });

    const user = new CognitoUser({
      Username: username,
      Pool: userPool
    });

    const authDetails = new AuthenticationDetails({
      Username: username,
      Password: password
    });

    user.authenticateUser(authDetails, {
      onSuccess: (data) => {
        console.log('OnSucces: ', data);
      },
      onFailure: (data) => {
        console.log('OnFailure: ', data);
      },
      newPasswordRequired: (data) => {
        console.log('OnFailure: ', data);
      }
    });
  }

  return (
    <Card>
      <CardContent >
        <TextField 
          id="outlined-basic" 
          label="Username" 
          variant="outlined" 
          type="email"
          value={ username }
          onChange={ event => setUsername(event.target.value) }
        />
        <TextField 
          id="outlined-basic" 
          label="Password" 
          variant="outlined" 
          type="password"
          value={ password }
          onChange={ event => setPassword(event.target.value) }
        />

        <Button variant="contained" onClick={ handleSubmitButton }>Submit</Button>
      </CardContent>
    </Card>
    // <Router>
    //   <div className="App">
    //     <NavBar />
    //     <Routes>
    //       <Route path="/accounts" element={<Accounts />} />
    //       <Route path="/dashboard" element={<Dashboard />} />
    //       <Route path="/investments" element={<Investments/>} />
    //       <Route path="/payments" element={<Payments/>} />
    //       <Route path="/settings" element={<Settings/>} />
    //       <Route path="/statements" element={<Statements/>} />
    //       <Route path="/transactions" element={<Transactions/>} />
    //     </Routes>
    //   </div>
    // </Router>
  );
}

export default App;
