import { CognitoUserPool } from "amazon-cognito-identity-js";

const UserPoolId = process.env.REACT_APP_USER_POOL_ID;
const ClientId = process.env.REACT_APP_CLIENT_ID;

console.table(process.env);

const poolData = {
  UserPoolId,
  ClientId
}

export default new CognitoUserPool(poolData);

