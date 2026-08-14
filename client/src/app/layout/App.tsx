import { CssBaseline, List, ListItem, ListItemText } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react"
import NavBar from "./NavBar";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    // fetch('https://localhost:5001/api/activities')
    //   .then(response => response.json)
    //   .then(data => setActivities(data))
    // The previous is native js function to get data

    axios.get<Activity[]>('https://localhost:5001/api/activities')
      .then(response => setActivities(response.data))

    return() => {}
  }, [])

  return (
    // <> is shorthand for <Fragment>
    <>
      <CssBaseline />
      <NavBar />
      <List>
        {activities.map((activity) => (
          <ListItem key={activity.id}>
            <ListItemText>{activity.title}</ListItemText>
          </ListItem>
        ))}
      </List>
    </>
  )
}

export default App
