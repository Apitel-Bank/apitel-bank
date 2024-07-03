export const isAuthenticated = async () => {
  const accessToken = sessionStorage.getItem("accessToken");
  if (accessToken) {
    let response = await fetch(
      "https://apitel-reporting.auth.eu-west-1.amazoncognito.com/oauth2/userInfo",
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      }
    );
    return response.status === 200;
  } else {
    console.log("failed");
    return false;
  }
};
