import { observer } from "mobx-react-lite";
import ExploreConnections from "./components/ExploreConnections";
import Heros from "./components/Heros";
import RailflowServices from "./components/RailflowServices";
import Carousel from "./components/Carousel";

export default function HomePage() {
  return (
    <div>
      <ExploreConnections />
      <Carousel />
      <Heros />
      <RailflowServices />
    </div>
  )
}