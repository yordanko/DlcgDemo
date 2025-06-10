import  { Col, Container, Row } from "react-bootstrap";
import GamesList from "../Components/GamesList";
import { useVideogames } from "../../lib/hooks/useVideogames";


export default function Dashboard() {
  const {videoGames, isPending} = useVideogames()
  if (isPending || !videoGames) {
    return <div className="text-center">Loading..</div>
  }
  return (
    
    <Container>
      <h3 className="text-center">Video Games Catalogue</h3>
      <Row>
        <Col xs={12} md={6} lg={4}>
          <GamesList/>
        </Col>
        <Col>
        Video Game Filters To Add
        </Col>
      </Row>
    </Container>
  )

}
