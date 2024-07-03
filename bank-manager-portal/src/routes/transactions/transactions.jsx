import { useEffect, useState } from "react";
export default function Transactions() {
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getTransactions = async () => {
      const accessToken = sessionStorage.getItem("accessToken");
      try {
        const response = await fetch(
          `${process.env.REACT_APP_BASE_URL}/accountTransactions`,
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
        console.log(data);
        setTransactions(data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    getTransactions();
  }, []);

  return (
    <section className="w-full h-full flex flex-col items-start p-8">
      <h1 className="text-2xl font-bold mb-4">Transactions</h1>
      <hr className="w-full mb-4" />
      {loading && <p className="text-blue-500">Loading...</p>}
      {error && <p className="text-red-500">Error: {error}</p>}
      {!loading && (
        <section id="transactions" className="flex-1 w-full text-left">
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Transaction ID
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Account ID
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Other Party ID
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Debit Amount
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  Credit Amount
                </th>
              </tr>
            </thead>
            <tbody>
              {transactions.map((transaction, index) => (
                <tr key={index} className="hover:bg-gray-100">
                  <td className="py-2 px-4 border-b border-gray-300">
                    {transaction.accountTransactionId}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {transaction.accountId}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {transaction.otherPartyId}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {transaction.debitInMibiBBDough}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {transaction.creditInMibiBBDough}
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
