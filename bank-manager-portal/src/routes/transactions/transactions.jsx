import { useEffect, useState } from "react";

export default function Transactions() {
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getTransactions = async () => {
      try {
        const response = await fetch("http://localhost:8080/accountTransactions");
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

    getTransactions();
  }, []);

  return (
    <section className="w-full h-full flex flex-col items-start p-8">
      <h1 className="text-2xl font-bold mb-4">Transactions</h1>
      <hr className="w-full mb-4" />
      {loading && <p className="text-blue-500">Loading...</p>}
      {error && <p className="text-red-500">Error: {error}</p>}
      {!loading && (
        <section id="transactions" className="flex-1 w-full">
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="py-2 px-4 border-b-2 border-gray-300">ID</th>
                <th className="py-2 px-4 border-b-2 border-gray-300">Date</th>
                <th className="py-2 px-4 border-b-2 border-gray-300">Amount</th>
                <th className="py-2 px-4 border-b-2 border-gray-300">From Account</th>
                <th className="py-2 px-4 border-b-2 border-gray-300">To Account</th>
                <th className="py-2 px-4 border-b-2 border-gray-300">Description</th>
              </tr>
            </thead>
            <tbody>
              {transactions.map((transaction) => (
                <tr key={transaction.id} className="hover:bg-gray-100">
                  <td className="py-2 px-4 border-b border-gray-300">{transaction.id}</td>
                  <td className="py-2 px-4 border-b border-gray-300">{transaction.date}</td>
                  <td className="py-2 px-4 border-b border-gray-300">{transaction.amount}</td>
                  <td className="py-2 px-4 border-b border-gray-300">{transaction.fromAccount}</td>
                  <td className="py-2 px-4 border-b border-gray-300">{transaction.toAccount}</td>
                  <td className="py-2 px-4 border-b border-gray-300">{transaction.description}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </section>
      )}
    </section>
  );
}
