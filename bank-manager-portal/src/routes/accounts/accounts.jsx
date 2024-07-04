import React, { useEffect, useState } from "react";
import { CircularProgress } from "@mui/material";
import { convertToBBDough } from "../../helpers/money";

export default function Accounts() {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [page, setPage] = useState(0);
  const [size, setSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);
  const [accountIdSearch, setAccountIdSearch] = useState("");
  const [customerIdSearch, setCustomerIdSearch] = useState("");

  useEffect(() => {
    const accessToken = sessionStorage.getItem("accessToken");

    const getData = async () => {
      setLoading(true);
      try {
        let url = `${process.env.REACT_APP_BASE_URL}/accounts?page=${page}&size=${size}`;
        if (accountIdSearch.trim() !== "") {
          url += `&accountId=${accountIdSearch.trim()}`;
        }
        if (customerIdSearch.trim() !== "") {
          url += `&customerId=${customerIdSearch.trim()}`;
        }

        const response = await fetch(url, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "application/json",
          },
        });
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const results = await response.json();
        setAccounts(results.content);
        setTotalPages(results.totalPages);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    getData();
  }, [page, size, accountIdSearch, customerIdSearch]);

  const handlePreviousPage = () => {
    setLoading(true);
    if (page > 0) {
      setPage(page - 1);
    }
  };

  const handleNextPage = () => {
    setLoading(true);
    if (page < totalPages - 1) {
      setPage(page + 1);
    }
  };

  const handlePageClick = (pageNum) => {
    setLoading(true);
    setPage(pageNum);
  };

  const getPageNumbers = () => {
    const maxPages = 5;
    const startPage = Math.max(0, page - maxPages);
    const endPage = Math.min(totalPages - 1, page + maxPages);
    const pages = [];
    if (startPage > 0) {
      pages.push(0); 
      if (startPage > 1) {
        pages.push("..."); 
      }
    }
    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }
    if (endPage < totalPages - 1) {
      if (endPage < totalPages - 2) {
        pages.push("..."); 
      }
      pages.push(totalPages - 1); 
    }
    return pages;
  };

  const handleAccountIdSearchChange = (event) => {
    setAccountIdSearch(event.target.value);
    setCustomerIdSearch(""); 
  };

  const handleCustomerIdSearchChange = (event) => {
    setCustomerIdSearch(event.target.value);
    setAccountIdSearch("");
  };


  return (
    <section className="w-full h-full flex flex-col items-start p-8">
      <h1 className="text-2xl font-bold mb-4">Accounts</h1>
      <hr className="w-full mb-4" />
      <div className="mb-4 w-full flex space-x-4">
        <input
          type="text"
          placeholder="Search account id..."
          className="border border-gray-300 rounded p-2 w-1/3"
          value={accountIdSearch}
          onChange={handleAccountIdSearchChange}
        />
        <input
          type="text"
          placeholder="Search customer id..."
          className="border border-gray-300 rounded p-2 w-1/3"
          value={customerIdSearch}
          onChange={handleCustomerIdSearchChange}
        />
      </div>
      {loading && (
        <div className="flex justify-center items-center h-32 w-full">
          <CircularProgress />
        </div>
      )}
      {error && <p className="text-red-500">Error: {error}</p>}
      {!loading && accounts.length === 0 && (
        <p className="text-gray-500">No accounts found.</p>
      )}
      {!loading && accounts.length > 0 && (
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
                    {account.balance !== null ? `Ð ${convertToBBDough(account.balance)}` : "0"}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <div className="flex gap-4 justify-between mt-4">
            <button
              onClick={handlePreviousPage}
              disabled={page === 0}
              className="px-4 py-2 bg-gray-300 rounded"
            >
              {"<"}
            </button>
            <div className="flex gap-2 flex-1 overflow-x-auto justify-center">
              {getPageNumbers().map((pageNum, index) =>
                pageNum === "..." ? (
                  <span key={index} className="px-2 py-2">
                    ...
                  </span>
                ) : (
                  <p
                    key={pageNum}
                    onClick={() => handlePageClick(pageNum)}
                    className={`px-0 py-2 rounded cursor-pointer font-semibold ${
                      page === pageNum ? "text-blue-500 underline" : "text-black"
                    }`}
                  >
                    {pageNum + 1}
                  </p>
                )
              )}
            </div>
            <button
              onClick={handleNextPage}
              disabled={page === totalPages - 1}
              className="px-4 py-2 bg-gray-300 rounded"
            >
              {">"}
            </button>
          </div>
        </section>
      )}
    </section>
  );
}
