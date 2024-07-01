import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import SideBar from '../sidebar/sidebar';
import { createTheme, ThemeProvider } from '@mui/material/styles';

export const theme = createTheme({
  palette: {
    primary: {
      main: '#004d40', // Custom primary color
    },
    secondary: {
      main: '#ffab00', // Custom secondary color
    },
  },
  typography: {
    h6: {
      fontWeight: 700, // Bold font weight for the title
    },
    button: {
      textTransform: 'none', // Disable uppercase transformation for buttons
    },
  },
  components: {
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundColor: '#004d40', // Custom background color for the AppBar
        },
      },
    },
    MuiToolbar: {
      styleOverrides: {
        root: {
          paddingLeft: '16px', // Custom padding for the toolbar
          paddingRight: '16px',
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          color: '#ffffff', // Custom color for buttons
        },
      },
    },
  },
});

export default function NavBar() {
  const [state, setState] = React.useState({
    top: false,
    left: false,
    bottom: false,
    right: false,
  });

  const toggleDrawer = (anchor, open) => (event) => {
    if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }

    setState({ ...state, [anchor]: open });
  };

  return (
    <ThemeProvider theme={theme}>
      <Box sx={{ flexGrow: 1 }}>
        <AppBar position="static">
          <Toolbar>
            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ mr: 2 }}
              onClick={toggleDrawer('left', true)}
            >
              <MenuIcon />
            </IconButton>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
              Retail Bank
            </Typography>
            <Button color="inherit">Login</Button>
          </Toolbar>
        </AppBar>
        <SideBar toggleDrawer={toggleDrawer} state={state} setState={setState} />
      </Box>
    </ThemeProvider>
  );
}
