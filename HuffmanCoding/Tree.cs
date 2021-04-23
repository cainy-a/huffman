namespace HuffmanCoding
{
	public class TreeRoot
	{
		public TreeNode LeftNode  { get; set; }
		public TreeNode RightNode { get; set; }
	}
	public class TreeNode
	{
		public TreeNode Left  { get; set; }
		public TreeNode Right { get; set; }
		public char?    Character { get; set; }
		public int      Count     { get; set; }

		public static explicit operator TreeRoot(TreeNode n)
			=> new() { LeftNode = n.Left, RightNode = n.Right };
	
		public static implicit operator TreeNode(TreeRoot r)
			=> new() { Left = r.LeftNode, Right = r.RightNode };
	}
}