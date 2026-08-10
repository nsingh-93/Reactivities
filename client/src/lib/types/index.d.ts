// Can easily make types using JsonToTs tool online
// Just get json response from api call and paste
// that into the tool and copy outpout to here for type

// interface IActivity = {
type Activity = {
  id: string;
  title: string;
  date: string;
  description: string;
  category: string;
  isCancelled: boolean;
  city: string;
  venue: string;
  latitude: number;
  longitude: number;
};
