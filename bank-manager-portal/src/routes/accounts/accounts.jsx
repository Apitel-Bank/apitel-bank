import { CircularProgress } from "@mui/material";
import { useEffect, useState } from "react";

export default function Accounts() {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getData = async () => {
      console.table(process.env)
      try {
        const response = await fetch(`${process.env.REACT_APP_BASE_URL}/accounts`);
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const results = await response.json();
        setAccounts(results);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    getData();
  }, []);

  return (
    <section className="w-full h-full flex flex-col items-start p-8">
      <h1 className="text-2xl font-bold mb-4">Accounts</h1>
      <hr className="w-full mb-4" />
      <div className="mb-4 w-full flex space-x-4">
        <input
          type="text"
          placeholder="Search account id"
          className="border border-gray-300 rounded p-2 w-1/3"
        />
        <select className="border border-gray-300 rounded p-2 w-1/3">
          <option value="">Filter by account type</option>
          <option value="savings">Savings</option>
          <option value="checking">Checking</option>
        </select>
      </div>
      {loading && (
        <div className="flex justify-center items-center h-32 w-full">
          <CircularProgress />
        </div>
      )}
      {error && <p className="text-red-500">Error: {error}</p>}
      {!loading && !error && (
        <section id="accounts" className="flex-1 w-full text-left">
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="py-2 px-4 border-b-2 border-gray-300">Name</th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Account ID
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Customer ID
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Available balance
                </th>
              </tr>
            </thead>
            <tbody>
              {accounts.map((account) => (
                <tr key={account.accountId} className="hover:bg-gray-100">
                  <td className="py-2 px-4 border-b border-gray-300">
                    {account.name}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {account.accountId}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {account.customerId}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300 text-green-700">
                    {"$ 2000"}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </section>
      )}
    </section>
  );
}
