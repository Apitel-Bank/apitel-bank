import * as React from 'react';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import AccountBalanceIcon from '@mui/icons-material/AccountBalance';
import PaymentsIcon from '@mui/icons-material/Payments';
import PaidIcon from '@mui/icons-material/Paid';
import StatementIcon from '@mui/icons-material/Description';
import InvestmentsIcon from '@mui/icons-material/TrendingUp';
import SettingsIcon from '@mui/icons-material/Settings';
import { ListItemText, Typography } from '@mui/material';

const primaryItems = [
  { text: "Accounts", icon: <AccountBalanceIcon /> },
  { text: "Transactions", icon: <PaidIcon /> },
  { text: "Payments", icon: <PaymentsIcon /> },
];

const secondaryItems = [
  { text: "Statements", icon: <StatementIcon /> },
  { text: "Investments", icon: <InvestmentsIcon /> },
  { text: "Settings", icon: <SettingsIcon /> },
];

export default function SideBar({ toggleDrawer, state }) {
  const list = (anchor) => (
    <Box
      sx={{ width: anchor === 'top' || anchor === 'bottom' ? 'auto' : 250 }}
      role="presentation"
      onClick={toggleDrawer(anchor, false)}
      onKeyDown={toggleDrawer(anchor, false)}
    >
      <List sx={{ display: 'flex', justifyContent: 'center' }}>
        <Typography variant="h5">Allan Walker</Typography>
      </List>
      <Divider />
      <List>
        {primaryItems.map((item, index) => (
          <ListItem  key={index} disablePadding>
            <ListItemButton href={`/${item.text.toLowerCase()}`}>
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        {secondaryItems.map((item, index) => (
          <ListItem href={`/${item.text.toLowerCase()}`} key={index} disablePadding>
            <ListItemButton href={`/${item.text.toLowerCase()}`}>
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
        onClose={toggleDrawer('left', false)}
      >
        {list('left')}
      </Drawer>
    </div>
  );
}
