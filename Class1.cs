using System;

public class Class1
{
	public Class1()
	{
      
	}

    //semihcelikol.com MenuStript kullanımı kod örneği;
    /// <summary>
    /// Default MenuStrip opitons.
    /// </summary>
    /// <param name="_form"></param>
    /// <returns>return MenuStrip</returns>
    public void MainMenu(Form _form)
    {
        MenuStrip m = new MenuStrip();
        ToolStripMenuItem fileItem = new ToolStripMenuItem("File");
        ToolStripMenuItem fileSubItem = new ToolStripMenuItem("Sign out");
        ToolStripMenuItem aboutItem = new ToolStripMenuItem("About");

        //ToolStripMenuItem options
        fileItem.DropDownItems.Add(fileSubItem);

        //MenuStript options
        m.Name = "MenuMain";
        m.Dock = DockStyle.Top;
        m.Items.Add(fileItem);
        m.Items.Add(aboutItem);

        //form controls add
        _form.Controls.Add(m);

        //Click menu control
        fileSubItem.Click += FileSubItem_Click;
        aboutItem.Click += AboutItem_Click;
    }

    //Sign out click control event
    private void FileSubItem_Click(object sender, EventArgs e)
    {
        this.exit();
    }
    //About click control event
    private void AboutItem_Click(object sender, EventArgs e)
    {
        this.about();
    }

}


