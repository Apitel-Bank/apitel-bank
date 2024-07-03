import { useEffect, useState } from "react";
import { TextField, Button, MenuItem, Select, InputLabel, FormControl, Box, Typography, Grid, CircularProgress, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from "@mui/material";
import './styles.css';

export default function Statements() {
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [accountId, setAccountId] = useState("");
  const [dateRange, setDateRange] = useState("30");

  const fetchTransactions = async (accountId, dateRange) => {
    try {
      const response = await fetch(`${process.env.REACT_APP_BASE_URL}/accountTransactions?accountId=${accountId}&dateRange=${dateRange}`);
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const data = await response.json();
      setTransactions(data);
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = () => {
    setLoading(true);
    fetchTransactions(accountId, dateRange);
  };

  return (
    <Box sx={{ p: 4 }}>
      <Typography variant="h4" mb={2}>Statements</Typography>
      <Grid container spacing={2} justifyContent="flex-end">
        <Grid item xs={12} md={4}>
          <FormControl fullWidth>
            <TextField
              label="Account ID"
              value={accountId}
              onChange={(e) => setAccountId(e.target.value)}
            />
          </FormControl>
        </Grid>
        <Grid item xs={12} md={4}>
          <FormControl fullWidth>
            <InputLabel>Date Range</InputLabel>
            <Select
              value={dateRange}
              onChange={(e) => setDateRange(e.target.value)}
            >
              <MenuItem value="30">30 Days</MenuItem>
              <MenuItem value="60">60 Days</MenuItem>
              <MenuItem value="90">90 Days</MenuItem>
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12} md={2}>
          <Button variant="contained" color="primary" fullWidth onClick={handleSearch}>
            Search
          </Button>
        </Grid>
      </Grid>

      {loading && <Box display="flex" justifyContent="center" mt={4}><CircularProgress /></Box>}
      {error && <Typography color="error" mt={2}>Error: {error}</Typography>}

      {!loading && !error && transactions.length > 0 && (
        <Box mt={4}>
          <FormControl fullWidth sx={{ mb: 2 }}>
            <Select defaultValue="All">
              <MenuItem value="All">All</MenuItem>
              <MenuItem value="Money In">Money In</MenuItem>
              <MenuItem value="Money Out">Money Out</MenuItem>
            </Select>
          </FormControl>
          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>Transaction ID</TableCell>
                  <TableCell>Account ID</TableCell>
                  <TableCell>Debit Amount</TableCell>
                  <TableCell>Credit Amount</TableCell>
                  <TableCell>Other Party ID</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {transactions.map((transaction, index) => (
                  <TableRow key={index} hover>
                    <TableCell>{transaction.accountTransactionId}</TableCell>
                    <TableCell>{transaction.accountId}</TableCell>
                    <TableCell>{transaction.debitInMibiBBDough}</TableCell>
                    <TableCell>{transaction.creditInMibiBBDough}</TableCell>
                    <TableCell>{transaction.otherPartyId}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Box>
      )}

      {!loading && !error && transactions.length === 0 && (
        <Typography mt={2}>No transactions found for the selected filters.</Typography>
      )}
    </Box>
  );
}

