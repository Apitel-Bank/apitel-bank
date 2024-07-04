import "./App.css";
import NavBar from "./components/navbar/navbar";
import Accounts from "./routes/accounts/accounts";
import Dashboard from "./routes/dashboard/dashbord";
import Transactions from "./routes/transactions/transactions";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./routes/login/login";
import DebitOrders from "./routes/debitOrders/debitOrders";

function App() {
  return (
    <Router>
      <div className="App flex flex-col">
        <NavBar />
        <div>
          <Routes className="flex-1 bg-green-400">
            <Route path="/" element={<Login />} />
            <Route path="/accounts" element={<Accounts />} />
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/transactions" element={<Transactions />} />
            <Route path="/debitorders" element={<DebitOrders />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
