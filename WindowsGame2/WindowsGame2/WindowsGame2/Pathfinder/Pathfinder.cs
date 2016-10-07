using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS {
    class Pathfinder {
        internal static List<IntPosition> getShortestPath(IntPosition start, IntPosition goal) {
            BinaryHeap<ExploredNode> openSet = new BinaryHeap<ExploredNode>();
            List<IntPosition> closedSet = new List<IntPosition>();

            Dictionary<IntPosition, IntPosition> cameFrom = new Dictionary<IntPosition, IntPosition>();

            openSet.Add(new ExploredNode(getHeuristic(start, goal),start));

            while (openSet.Count != 0){
                ExploredNode node = openSet.Remove();
                if (!node.Position.Equals(goal)){
                    foreach(IntPosition pos in Map.getAdjecentSquares(node.Position)){
                        if (!cameFrom.ContainsKey(pos)){
                            if (Map.isWalkable(pos)){
                                openSet.Add(new ExploredNode(getHeuristic(pos, goal), pos));
                                cameFrom.Add(pos, node.Position);
                            }
                        }
                    }
                } else {
                    return createPath(node, cameFrom, start);
                }
            }
            throw new System.Exception();
        }

        private static List<IntPosition> createPath(ExploredNode node, Dictionary<IntPosition, IntPosition> cameFrom, IntPosition start) {
            List<IntPosition> path = new List<IntPosition>();
            IntPosition from = cameFrom[node.Position];
            while (!(from.XPos == start.XPos && from.YPos == start.YPos)){
                path.Add(from);
                from = cameFrom[from];
            }
            return path;
        }

        private static int getHeuristic(IntPosition start, IntPosition goal) {
            int width = Math.Abs(start.XPos - goal.XPos);
            int height = Math.Abs(start.YPos - goal.YPos);
            return (int)(Math.Sqrt(width*width + height*height))*10;
        }
    }
}
