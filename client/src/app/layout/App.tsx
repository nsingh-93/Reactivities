import { Box, Container, CssBaseline, Typography } from "@mui/material";
import { useState } from "react";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { useActivities } from "../../lib/hooks/useActivities";

function App() {
  //const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<
    Activity | undefined
  >(undefined);
  const [editMode, setEditMode] = useState(false);

  // The useEffect becomes too much so it gets shifted to our own hook
  // that is in another file and then brought into here like useEffect
  // useEffect(() => {
  //   // fetch('https://localhost:5001/api/activities')
  //   //   .then(response => response.json)
  //   //   .then(data => setActivities(data))
  //   // The previous is native js function to get data

  //   axios
  //     .get<Activity[]>("https://localhost:5001/api/activities")
  //     .then((response) => setActivities(response.data));

  //   return () => {};
  // }, []);

  const {activities, isPending} = useActivities();

  const handleSelectActivity = (id: string) => {
    setSelectedActivity(activities!.find((x) => x.id === id));
  };

  const handleCancelSelectActivity = () => {
    setSelectedActivity(undefined);
  };

  const handleOpenForm = (id?: string) => {
    if (id) handleSelectActivity(id);
    else handleCancelSelectActivity();
    setEditMode(true);
  };

  const handleFormClose = () => {
    setEditMode(false);
  };

  return (
    // <> is shorthand for <Fragment>
    <Box sx={{ bgcolor: "#eeeeee", minHeight: "100vh" }}>
      <CssBaseline />
      <NavBar openForm={handleOpenForm} />
      <Container maxWidth="xl" sx={{ mt: 3 }}>
        {!activities || isPending ? (
          <Typography>Loading...</Typography>
        ) : (
          <ActivityDashboard
            activities={activities}
            selectActivity={handleSelectActivity}
            cancelSelectActivity={handleCancelSelectActivity}
            selectedActivity={selectedActivity}
            editMode={editMode}
            openForm={handleOpenForm}
            closeForm={handleFormClose}
          />
        )}
      </Container>
    </Box>
  );
}

export default App;
