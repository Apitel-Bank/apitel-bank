import * as React from "react";
import Box from "@mui/material/Box";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import Divider from "@mui/material/Divider";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import AccountBalanceIcon from "@mui/icons-material/AccountBalance";
import PaidIcon from "@mui/icons-material/Paid";
import StatementIcon from "@mui/icons-material/Description";
import { ListItemText, Typography } from "@mui/material";
import { Link } from "react-router-dom";

const primaryItems = [
  { text: "Dashboard", icon: <PaidIcon /> },
  { text: "Accounts", icon: <AccountBalanceIcon /> },
  { text: "Transactions", icon: <PaidIcon /> },
];

const secondaryItems = [
  { text: "DebitOrders", icon: <StatementIcon /> },
  // { text: "Statements", icon: <StatementIcon /> },
];

export default function SideBar({ toggleDrawer, state }) {
  const list = (anchor) => (
    <Box
      sx={{ width: anchor === "top" || anchor === "bottom" ? "auto" : 250 }}
      role="presentation"
      onClick={toggleDrawer(anchor, false)}
      onKeyDown={toggleDrawer(anchor, false)}
    >
      <List sx={{ display: "flex", justifyContent: "center" }}>
        <Typography variant="h5">Allan Walker</Typography>
      </List>
      <Divider />
      <List>
        {primaryItems.map((item, index) => (
          <ListItem key={index} disablePadding>
            <ListItemButton component={Link} to={`/${item.text.toLowerCase()}`}>
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        {secondaryItems.map((item, index) => (
          <ListItem key={index} disablePadding>
            <ListItemButton component={Link} to={`/${item.text.toLowerCase()}`}>
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Box>
  );

  return (
    <div>
      <Drawer
        anchor="left"
        open={state.left}
        onClose={toggleDrawer("left", false)}
      >
        {list("left")}
      </Drawer>
    </div>
  );
}
