import { CognitoUserPool } from "amazon-cognito-identity-js";

const UserPoolId = process.env.UserPoolId;
const ClientId = process.env.ClientId;

const poolData = {
  UserPoolId,
  ClientId
}

export default new CognitoUserPool(poolData);

