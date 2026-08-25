@tool
extends EditorScript

func _run():
	var editor_selection: EditorSelection = EditorInterface.get_selection()
	var selected_nodes: Array[Node] = editor_selection.get_selected_nodes()

	if selected_nodes.size() == 0:
		print("Select a MultiMeshInstance3D node.")
		return

	var node = selected_nodes[0]
	if not node is MultiMeshInstance3D:
		print("Selected node is not a MultiMeshInstance3D.")
		return

	var old_mm: MultiMesh = node.multimesh
	if !old_mm:
		print("MultiMesh is null.")
		return
		
	var filtered_transforms: Array[Transform3D] = []
	for i in old_mm.instance_count:
		var orignal_transform: Transform3D = old_mm.get_instance_transform(i)
		var up = orignal_transform.basis.y.normalized()
		if up.dot(Vector3.UP) > 0.98:
			filtered_transforms.append(orignal_transform)


	var new_mm: MultiMesh = MultiMesh.new()
	new_mm.transform_format = old_mm.transform_format
	new_mm.mesh = old_mm.mesh 
	new_mm.instance_count = filtered_transforms.size()
	
	for i in filtered_transforms.size():
		new_mm.set_instance_transform(i, filtered_transforms[i])

	node.multimesh = new_mm
	print("Filtered MultiMesh: Kept %d instances." % filtered_transforms.size())
