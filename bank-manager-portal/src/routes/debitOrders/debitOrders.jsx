import { useEffect, useState } from "react";
export default function DebitOrders() {
  const [debitOrders, setDebitOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getDebitOrders = async () => {
      const accessToken = sessionStorage.getItem("accessToken");
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
        setDebitOrders(data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    getDebitOrders();
  }, []);

  return (
    <section className="w-full h-full flex flex-col items-start p-8">
      <h1 className="text-2xl font-bold mb-4">Debit Orders</h1>
      <hr className="w-full mb-4" />
      {loading && <p className="text-blue-500">Loading...</p>}
      {error && <p className="text-red-500">Error: {error}</p>}
      {!loading && (
        <section id="debitOrders" className="flex-1 w-full text-left">
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  DebitOrder ID
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  AmountInMibiDDough
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  AccountId
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  DayInTheMonth
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">EndsAt</th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  CancelledAt
                </th>
                <th className="py-2 px-4 border-b-2 border-gray-300">
                  DebitOrderRecipientId
                </th>
              </tr>
            </thead>
            <tbody>
              {debitOrders.map((debitOrder, index) => (
                <tr key={index} className="hover:bg-gray-100">
                  <td className="py-2 px-4 border-b border-gray-300">
                    {debitOrder.debitOrderId}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {debitOrder.amountInMibiBBDough}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {debitOrder.accountId}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {debitOrder.dayInTheMonth}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {debitOrder.endsAt}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {debitOrder.CancelledAt}
                  </td>
                  <td className="py-2 px-4 border-b border-gray-300">
                    {debitOrder.debitOrderRecipientId}
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
